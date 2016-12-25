using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Inventory.Backend.Repositories;
using System.Threading.Tasks;
using Inventory.Backend.Entities;
using Inventory.Backend.Models;

namespace Inventory.Backend.Controllers {
    [Authorize]
    [IdentityBasicAuthentication]
    [RoutePrefix("services")]
    public class ServicesController : ApiController {
        #region Fields
        private ModelFactory _ModelFactory;
        private IInventoryRepository _InventoryRepository;
        #endregion

        #region Properties
        protected ModelFactory TheModelFactory {
            get {
                if (_ModelFactory == null) {
                    _ModelFactory = new ModelFactory();
                }
                return _ModelFactory;
            }
        }

        protected IInventoryRepository TheInventoryRepository {
            get {
                return _InventoryRepository;
            }
        }
        #endregion

        #region Constructor
        public ServicesController(IInventoryRepository repository) {
            _InventoryRepository = repository;
        }
        #endregion

        #region Store Resources
        [Route("stores")]
        public async Task<IHttpActionResult> GetStores() {
            try {
                IEnumerable<Store> stores = await TheInventoryRepository.FindStoresAsync();
                if (stores != null && stores.Count() > 0) {
                    var vModels = stores.Select(u => this.TheModelFactory.Create(u));
                    return Ok(vModels);
                }
            } catch (Exception ex) {
                return InternalServerError(ex);
            }
            return NotFound();
        }

        [Route("stores/{storeId}", Name = "GetStoreById")]
        public async Task<IHttpActionResult> GetStoreById(int storeId) {
            if (storeId <= 0) {
                return BadRequest();
            }
            try {
                Store store = await TheInventoryRepository.FindStoreAsync(storeId);
                if (store != null) {
                    return Ok(TheModelFactory.Create(store));
                } else {
                    return NotFound();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }

        [Route("stores/create")]
        public async Task<IHttpActionResult> CreateStore(StoreModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var store = new Store {
                    StoreID = model.StoreID,
                    Name = model.Name,
                    Address = model.Address
                };

                bool success = await TheInventoryRepository.CreateStoreAsync(store);
                if (success) {
                    Uri vLocation = new Uri(Url.Link("GetStoreById", new { storeId = store.StoreID }));
                    return Created(vLocation, TheModelFactory.Create(store));
                } else {
                    return BadRequest();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }

        [Route("stores/update")]
        public async Task<IHttpActionResult> UpdateStore(StoreModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var store = new Store {
                    StoreID = model.StoreID,
                    Name = model.Name,
                    Address = model.Address
                };

                bool success = await TheInventoryRepository.UpdateStoreAsync(store);
                if (success) {
                    return Ok(TheModelFactory.Create(store));
                } else {
                    return InternalServerError();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }

        [Route("stores/delete/{storeId}")]
        public async Task<IHttpActionResult> DeleteStore(int storeId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                bool success = await TheInventoryRepository.DeleteStoreAsync(storeId);
                if (success) {
                    return Ok();
                } else {
                    return InternalServerError();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }

        #endregion

        #region Articles Resources
        [Route("articles")]
        public async Task<IHttpActionResult> GetArticles() {
            try {
                IEnumerable<Article> articles = await TheInventoryRepository.FindArticlesAsync();
                if (articles != null && articles.Count() > 0) {
                    var vModels = articles.Select(u => this.TheModelFactory.Create(u));
                    return Ok(vModels);
                }
            } catch (Exception ex) {
                return InternalServerError(ex);
            }
            return NotFound();
        }

        [Route("articles/stores/{storeID}")]
        public async Task<IHttpActionResult> GetArticlesFromStore(int storeID) {
            if (storeID <= 0) {
                return BadRequest();
            }
            try {
                IEnumerable<Article> articles = await TheInventoryRepository.FindArticlesAsync(storeID);
                if (articles != null && articles.Count() > 0) {
                    var vModels = articles.Select(u => this.TheModelFactory.Create(u));
                    return Ok(vModels);
                }
            } catch (Exception ex) {
                return InternalServerError(ex);
            }
            return NotFound();
        }

        [Route("articles/{articleId}", Name = "GetArticleById")]
        public async Task<IHttpActionResult> GetArticleById(int articleId) {
            if (articleId <= 0) {
                return BadRequest();
            }
            try {
                Article article = await TheInventoryRepository.FindArticleAsync(articleId);
                if (article != null) {
                    return Ok(TheModelFactory.Create(article));
                } else {
                    return NotFound();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }

        [Route("articles/create")]
        public async Task<IHttpActionResult> CreateArticle(ArticleModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var article = new Article {
                    StoreID = model.StoreID,
                    ArticleID = model.ArticleID,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    TotalInShelf = model.TotalInShelf,
                    TotalInVault = model.TotalInVault
                };

                bool success = await TheInventoryRepository.CreateArticleAsync(article);
                if (success) {
                    article.Store = await TheInventoryRepository.FindStoreAsync(article.StoreID);
                    Uri vLocation = new Uri(Url.Link("GetArticleById", new { articleId = article.ArticleID }));
                    return Created(vLocation, TheModelFactory.Create(article));
                } else {
                    return BadRequest();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }

        [Route("articles/update")]
        public async Task<IHttpActionResult> UpdateArticle(ArticleModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var article = new Article {
                    StoreID = model.StoreID,
                    ArticleID = model.ArticleID,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    TotalInShelf = model.TotalInShelf,
                    TotalInVault = model.TotalInVault
                };

                bool success = await TheInventoryRepository.UpdateArticleAsync(article);
                if (success) {
                    article.Store = await TheInventoryRepository.FindStoreAsync(article.StoreID);
                    return Ok(TheModelFactory.Create(article));
                } else {
                    return InternalServerError();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }

        [Route("articles/delete/{articleId}")]
        public async Task<IHttpActionResult> DeleteArticle(int articleId) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                bool success = await TheInventoryRepository.DeleteArticleAsync(articleId);
                if (success) {
                    return Ok();
                } else {
                    return InternalServerError();
                }
            } catch (Exception vEx) {
                return InternalServerError(vEx);
            }
        }
        #endregion
    }
}
