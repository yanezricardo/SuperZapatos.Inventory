using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Inventory.Backend.Entities;

namespace Inventory.Backend.Repositories {
    public class InventoryRepository : IInventoryRepository {
        #region Stores
        public async Task<IEnumerable<Store>> FindStoresAsync() {
            IEnumerable<Store> result = null;
            await Task.Run(() => {
                using (var context = new InventoryContext()) {
                    result = context.Stores.ToList();
                }
            });
            return result;
        }

        public async Task<Store> FindStoreAsync(int storeId) {
            Store result = null;
            await Task.Run(() => {
                using (var context = new InventoryContext()) {
                    result = context.Stores.Where(i => i.StoreID == storeId).FirstOrDefault();
                }
            });
            return result;
        }

        public async Task<bool> CreateStoreAsync(Store store) {
            bool result = false;
            using (var context = new InventoryContext()) {
                context.Stores.Add(store);
                result = await context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> UpdateStoreAsync(Store store) {
            bool result = false;
            using (var context = new InventoryContext()) {
                context.Stores.Attach(store);
                context.Entry<Store>(store).State = System.Data.Entity.EntityState.Modified;
                result = await context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> DeleteStoreAsync(int storeId) {
            bool result = false;
            using (var context = new InventoryContext()) {
                var store = await context.Stores.FindAsync(storeId);
                if (store != null) {
                    context.Stores.Attach(store);
                    context.Entry<Store>(store).State = System.Data.Entity.EntityState.Deleted;
                    result = await context.SaveChangesAsync() > 0;
                }

            }
            return result;
        }
        #endregion

        #region Articles
        public async Task<IEnumerable<Article>> FindArticlesAsync() {
            IEnumerable<Article> result = null;
            await Task.Run(() => {
                using (var context = new InventoryContext()) {
                    result = context.Articles.Include("Store").ToList();
                }
            });
            return result;
        }

        public async Task<IEnumerable<Article>> FindArticlesAsync(int storeID) {
            IEnumerable<Article> result = null;
            await Task.Run(() => {
                using (var context = new InventoryContext()) {
                    result = context.Articles.Include("Store").Where(i => i.StoreID == storeID).ToList();
                }
            });
            return result;
        }

        public async Task<Article> FindArticleAsync(int articleId) {
            Article result = null;
            await Task.Run(() => {
                using (var context = new InventoryContext()) {
                    result = context.Articles.Include("Store").Where(i => i.ArticleID == articleId).FirstOrDefault();
                }
            });
            return result;
        }

        public async Task<bool> CreateArticleAsync(Article article) {
            bool result = false;
            using (var context = new InventoryContext()) {
                context.Articles.Add(article);
                result = await context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> UpdateArticleAsync(Article article) {
            bool result = false;
            using (var context = new InventoryContext()) {
                context.Articles.Attach(article);
                context.Entry<Article>(article).State = System.Data.Entity.EntityState.Modified;
                result = await context.SaveChangesAsync() > 0;
            }
            return result;
        }

        public async Task<bool> DeleteArticleAsync(int articleId) {
            bool result = false;
            using (var context = new InventoryContext()) {
                var article = await context.Articles.FindAsync(articleId);
                if (article != null) {
                    context.Articles.Attach(article);
                    context.Entry<Article>(article).State = System.Data.Entity.EntityState.Deleted;
                    result = await context.SaveChangesAsync() > 0;
                }

            }
            return result;
        } 
        #endregion
    }
}