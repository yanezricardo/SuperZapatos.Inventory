using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Inventory.Site.ViewModels {
    public class ArticleViewModel {
        public int StoreID { get; set; }

        public int ArticleID { get; set; }

        [DisplayName("Tienda")]
        public string StoreName { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Precio")]
        public decimal Price { get; set; }

        [DisplayName("Total en mostrador")]
        public decimal TotalInShelf { get; set; }

        [DisplayName("Total en almacen")]
        public decimal TotalInVault { get; set; }
    }
}