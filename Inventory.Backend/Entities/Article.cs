using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Backend.Entities {
    public class Article {
        public int StoreID { get; set; }
        public int ArticleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal TotalInShelf { get; set; }
        public decimal TotalInVault { get; set; }


        public virtual Store Store { get; set; }
    }
}