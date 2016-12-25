using Inventory.Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Inventory.Backend.Models {
    public class ModelFactory {
        public StoreModel Create(Store entity) {
            return new StoreModel {
                StoreID = entity.StoreID,
                Name = entity.Name,
                Address = entity.Address,
            };
        }

        public ArticleModel Create(Article entity) {
            return new ArticleModel {
                StoreID = entity.StoreID,
                ArticleID = entity.ArticleID,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                TotalInShelf = entity.TotalInShelf,
                TotalInVault = entity.TotalInVault,
                StoreName = entity.Store == null ? string.Empty : entity.Store.Name
            };
        }
    }
}