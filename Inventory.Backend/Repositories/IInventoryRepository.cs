using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Inventory.Backend.Entities;

namespace Inventory.Backend.Repositories {
    public interface IInventoryRepository {
        Task<IEnumerable<Store>> FindStoresAsync();
        Task<Store> FindStoreAsync(int storeId);
        Task<bool> CreateStoreAsync(Store store);
        Task<bool> UpdateStoreAsync(Store store);
        Task<bool> DeleteStoreAsync(int storeId);

        Task<IEnumerable<Article>> FindArticlesAsync();
        Task<IEnumerable<Article>> FindArticlesAsync(int storeID);
        Task<Article> FindArticleAsync(int articleId);
        Task<bool> CreateArticleAsync(Article article);
        Task<bool> UpdateArticleAsync(Article article);
        Task<bool> DeleteArticleAsync(int articleId);
    }
}