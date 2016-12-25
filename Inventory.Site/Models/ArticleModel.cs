using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Site.Models {
    public class ArticleModel {
        [JsonProperty(PropertyName = "storeid")]
        public int StoreID { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int ArticleID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "total_in_shelf")]
        public decimal TotalInShelf { get; set; }

        [JsonProperty(PropertyName = "total_in_vault")]
        public decimal TotalInVault { get; set; }

        [JsonProperty(PropertyName = "store_name")]
        public string StoreName { get; set; }
    }
}