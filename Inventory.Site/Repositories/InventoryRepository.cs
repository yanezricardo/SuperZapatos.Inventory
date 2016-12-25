using Inventory.Site.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inventory.Site.Repositories {
    public class InventoryRepository : ServiceClientBase, IInventoryRepository {
        #region Fields
        private string _BackendUrl = null;
        #endregion

        #region Properties
        public string BackendUrl {
            get {
                if (string.IsNullOrWhiteSpace(_BackendUrl)) {
                    _BackendUrl = ConfigurationManager.AppSettings["BackendUrl"];
                }
                return _BackendUrl;
            }
        }
        #endregion

        #region Stores
        public async Task<StoreListServiceResponse> FindStoresAsync() {
            StoreListServiceResponse result = new StoreListServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl)) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.GetAsync("services/stores");
                result = await response.Content.ReadAsAsync<StoreListServiceResponse>();
            }

            return result;
        }

        public async Task<StoreServiceResponse> FindStoreAsync(int storeId) {
            StoreServiceResponse result = new StoreServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || storeId <= 0) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.GetAsync("services/stores/" + storeId);
                result = await response.Content.ReadAsAsync<StoreServiceResponse>();
            }
            return result;
        }

        public async Task<StoreServiceResponse> CreateStoreAsync(StoreModel store) {
            StoreServiceResponse result = new StoreServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || store == null) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.PostAsJsonAsync("services/stores/create", store);
                result = await response.Content.ReadAsAsync<StoreServiceResponse>();
            }
            return result;
        }

        public async Task<StoreServiceResponse> UpdateStoreAsync(StoreModel store) {
            StoreServiceResponse result = new StoreServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || store == null) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.PostAsJsonAsync("services/stores/update", store);
                result = await response.Content.ReadAsAsync<StoreServiceResponse>();
            }
            return result;
        }

        public async Task<StoreServiceResponse> DeleteStoreAsync(int storeId) {
            StoreServiceResponse result = new StoreServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || storeId <= 0) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.DeleteAsync("services/stores/delete/" + storeId);
                result = await response.Content.ReadAsAsync<StoreServiceResponse>();
            }
            return result;
        }
        #endregion

        #region Articles
        public async Task<ArticleListServiceResponse> FindArticlesAsync() {
            ArticleListServiceResponse result = new ArticleListServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl)) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.GetAsync("services/articles");
                result = await response.Content.ReadAsAsync<ArticleListServiceResponse>();
            }
            return result;
        }

        public async Task<ArticleListServiceResponse> FindArticlesAsync(int storeId) {
            ArticleListServiceResponse result = new ArticleListServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || storeId <= 0) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.GetAsync("services/articles/stores/" + storeId);
                result = await response.Content.ReadAsAsync<ArticleListServiceResponse>();
            }
            return result;
        }

        public async Task<ArticleServiceResponse> FindArticleAsync(int articleId) {
            ArticleServiceResponse result = new ArticleServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || articleId <= 0) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.GetAsync("services/articles/" + articleId);
                result = await response.Content.ReadAsAsync<ArticleServiceResponse>();
            }
            return result;
        }

        public async Task<ArticleServiceResponse> CreateArticleAsync(ArticleModel article) {
            ArticleServiceResponse result = new ArticleServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || article == null) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.PostAsJsonAsync("services/articles/create", article);
                result = await response.Content.ReadAsAsync<ArticleServiceResponse>();
            }
            return result;
        }

        public async Task<ArticleServiceResponse> UpdateArticleAsync(ArticleModel article) {
            ArticleServiceResponse result = new ArticleServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || article == null) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.PostAsJsonAsync("services/articles/update", article);
                result = await response.Content.ReadAsAsync<ArticleServiceResponse>();
            }
            return result;
        }

        public async Task<ArticleServiceResponse> DeleteArticleAsync(int articleId) {
            ArticleServiceResponse result = new ArticleServiceResponse();
            if (string.IsNullOrWhiteSpace(BackendUrl) || articleId <= 0) {
                return result;
            }
            using (var client = CreateHttpClient(BackendUrl, "application/json")) {
                HttpResponseMessage response = await client.DeleteAsync("services/articles/delete/" + articleId);
                result = await response.Content.ReadAsAsync<ArticleServiceResponse>();
            }
            return result;
        }
        #endregion
    }
}