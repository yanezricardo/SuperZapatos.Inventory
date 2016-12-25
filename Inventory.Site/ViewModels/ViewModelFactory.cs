using Inventory.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Site.ViewModels {
    public class ViewModelFactory {

        public StoreViewModel Create(StoreModel model) {
            if(model == null) {
                model = new StoreModel();
            }
            return new StoreViewModel {
                StoreID = model.StoreID,
                Name = model.Name,
                Address = model.Address
            };
        }

        public ArticleViewModel Create(ArticleModel model) {
            if (model == null) {
                model = new ArticleModel();
            }
            return new ArticleViewModel {
                StoreID = model.StoreID,
                ArticleID = model.ArticleID,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                TotalInShelf = model.TotalInShelf,
                TotalInVault = model.TotalInVault,
                StoreName = model.StoreName
            };
        }
    }
}