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
    public class StoreController : BaseController {
        public StoreController(IInventoryRepository repository) : base(repository) {
        }

        public async Task<ActionResult> Index() {
            try {
                var response = await TheInventoryRepository.FindStoresAsync();
                var viewModels = new List<StoreViewModel>();
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
            var response = await TheInventoryRepository.FindStoreAsync(id);
            if (!response.Success) {
                return HttpNotFound();
            }
            var viewModel = TheViewModelFactory.Create(response.Result);
            var articlesResponse = await TheInventoryRepository.FindArticlesAsync(viewModel.StoreID);
            if (articlesResponse.Success && articlesResponse.Result != null) {
                viewModel.Articles = articlesResponse.Result.ConvertAll(i => TheViewModelFactory.Create(i));
            }else {
                viewModel.Articles = new List<ArticleViewModel>();
            }
            return View(viewModel);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(StoreViewModel viewModel) {
            try {
                if (ModelState.IsValid) {
                    StoreModel model = new StoreModel();
                    if (TryUpdateModel(model)) {
                        var response = await TheInventoryRepository.CreateStoreAsync(model);
                        if (response.Success) {
                            return Redirect("Index");
                        } else {
                            ModelState.AddModelError(string.Empty, string.Format("Error: {0} - {1}", response.ErrorCode, response.ErrorMessage));
                        }
                    }
                }
                return View(viewModel);
            } catch (Exception ex) {
                Debug.Write(ex.ToString());
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id) {
            var response = await TheInventoryRepository.FindStoreAsync(id);
            if (!response.Success) {
                return HttpNotFound();
            }
            var viewModel = TheViewModelFactory.Create(response.Result);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, StoreViewModel viewModel) {
            try {
                if (ModelState.IsValid) {
                    StoreModel model = new StoreModel();
                    if (TryUpdateModel(model)) {
                        model.StoreID = id;
                        var response = await TheInventoryRepository.UpdateStoreAsync(model);
                        if (response.Success) {
                            return RedirectToAction("Index");
                        } else {
                            ModelState.AddModelError(string.Empty, string.Format("Error: {0} - {1}", response.ErrorCode, response.ErrorMessage));
                        }
                    }
                }
                return View(viewModel);
            } catch {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id) {
            var response = await TheInventoryRepository.FindStoreAsync(id);
            if (!response.Success) {
                return HttpNotFound();
            }
            var viewModel = TheViewModelFactory.Create(response.Result);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, StoreViewModel viewModel) {
            try {
                var response = await TheInventoryRepository.DeleteStoreAsync(id);
                if (response.Success) {
                    return RedirectToAction("Index");
                } else {
                    ModelState.AddModelError(string.Empty, string.Format("Error: {0} - {1}", response.ErrorCode, response.ErrorMessage));
                }
                return View(viewModel);
            } catch (Exception ex) {
                Debug.Write(ex.ToString());
                return View();
            }
        }
    }
}