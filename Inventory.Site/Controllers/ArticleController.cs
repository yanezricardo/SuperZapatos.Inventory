using Inventory.Site.Models;
using Inventory.Site.Repositories;
using Inventory.Site.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inventory.Site.Controllers {
    [Authorize]
    public class ArticleController : BaseController {
        public ArticleController(IInventoryRepository repository) : base(repository) {
        }

        public async Task<ActionResult> Index() {
            try {
                var response = await TheInventoryRepository.FindArticlesAsync();
                var viewModels = new List<ArticleViewModel>();
                if (response.Success && response.Result != null) {
                    viewModels = response.Result.ConvertAll(i => TheViewModelFactory.Create(i));
                }
                return View(viewModels);
            } catch (Exception ex) {
                Debug.Write(ex.ToString());
                return View();
            }
        }

        public async Task<ActionResult> Details(int id) {
            var response = await TheInventoryRepository.FindArticleAsync(id);
            if (!response.Success) {
                return HttpNotFound();
            }
            var viewModel = TheViewModelFactory.Create(response.Result);
            return View(viewModel);
        }

        public async Task<ActionResult> Create(int? storeId) {
            var stores = await GetStoresList(storeId);
            ViewBag.Stores = stores;
            ViewBag.StoreListEnabled = storeId == null ? "enable" : "disabled";
            return View(new ArticleViewModel() { StoreID = storeId.GetValueOrDefault() });
        }

        [HttpPost]
        public async Task<ActionResult> Create(int? storeId, ArticleViewModel viewModel) {
            try {
                if (ModelState.IsValid) {
                    ArticleModel model = new ArticleModel();
                    if (TryUpdateModel(model)) {
                        var response = await TheInventoryRepository.CreateArticleAsync(model);
                        if (response.Success) {
                            if (storeId != null) {
                                return RedirectToAction("Details", "Store", new { id = storeId.GetValueOrDefault() });
                            } else {
                                Redirect("Index");
                            }
                        } else {
                            ModelState.AddModelError(string.Empty, string.Format("Error: {0} - {1}", response.ErrorCode, response.ErrorMessage));
                        }
                    }
                }
                ViewBag.Stores = await GetStoresList(storeId);
                return View(viewModel);
            } catch (Exception ex) {
                Debug.Write(ex.ToString());
                return View();
            }
        }

        public async Task<ActionResult> Edit(int? storeId, int id) {
            var response = await TheInventoryRepository.FindArticleAsync(id);
            if (!response.Success) {
                return HttpNotFound();
            }
            ViewBag.Stores = await GetStoresList(storeId);
            var viewModel = TheViewModelFactory.Create(response.Result);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int? storeId, int id, ArticleViewModel viewModel) {
            try {
                if (ModelState.IsValid) {
                    ArticleModel model = new ArticleModel();
                    if (TryUpdateModel(model)) {
                        model.ArticleID = id;
                        var response = await TheInventoryRepository.UpdateArticleAsync(model);
                        if (response.Success) {
                            if (storeId != null) {
                                return RedirectToAction("Details", "Store", new { id = storeId.GetValueOrDefault() });
                            } else {
                                return RedirectToAction("Index");
                            }
                        } else {
                            ModelState.AddModelError(string.Empty, string.Format("Error: {0} - {1}", response.ErrorCode, response.ErrorMessage));
                        }
                    }
                }
                ViewBag.Stores = await GetStoresList(storeId);
                return View(viewModel);
            } catch {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int? storeId, int id) {
            var response = await TheInventoryRepository.FindArticleAsync(id);
            if (!response.Success) {
                return HttpNotFound();
            }
            var viewModel = TheViewModelFactory.Create(response.Result);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? storeId, int id, ArticleViewModel viewModel) {
            try {
                var response = await TheInventoryRepository.DeleteArticleAsync(id);
                if (response.Success) {
                    if (storeId != null) {
                        return RedirectToAction("Details", "Store", new { id = storeId.GetValueOrDefault() });
                    } else {
                        return RedirectToAction("Index");
                    }
                } else {
                    ModelState.AddModelError(string.Empty, string.Format("Error: {0} - {1}", response.ErrorCode, response.ErrorMessage));
                }
                return View(viewModel);
            } catch (Exception ex) {
                Debug.Write(ex.ToString());
                return View();
            }
        }

        private async Task<SelectList> GetStoresList(int? storeId) {
            SelectList result = null;
            var response = await TheInventoryRepository.FindStoresAsync();
            if (response.Success && response.Result != null) {
                var storeList = response.Result.ConvertAll(i => TheViewModelFactory.Create(i));
                result = new SelectList(storeList, "StoreID", "Name");
                if (storeId != null) {
                    var currentStore = result.Where(i => i.Value == storeId.GetValueOrDefault().ToString()).FirstOrDefault();
                    if (currentStore != null) {
                        currentStore.Selected = true;
                    }
                }
            }
            return result;
        }
    }
}