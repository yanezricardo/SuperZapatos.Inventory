using Inventory.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Inventory.Site.Repositories {
    public interface IInventoryRepository {
        Task<StoreListServiceResponse> FindStoresAsync();
        Task<StoreServiceResponse> FindStoreAsync(int storeId);
        Task<StoreServiceResponse> CreateStoreAsync(StoreModel store);
        Task<StoreServiceResponse> UpdateStoreAsync(StoreModel store);
        Task<StoreServiceResponse> DeleteStoreAsync(int storeId);

        Task<ArticleListServiceResponse> FindArticlesAsync();
        Task<ArticleListServiceResponse> FindArticlesAsync(int storeID);
        Task<ArticleServiceResponse> FindArticleAsync(int articleId);
        Task<ArticleServiceResponse> CreateArticleAsync(ArticleModel article);
        Task<ArticleServiceResponse> UpdateArticleAsync(ArticleModel article);
        Task<ArticleServiceResponse> DeleteArticleAsync(int articleId);
    }
}