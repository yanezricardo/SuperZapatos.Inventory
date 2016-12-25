using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Site.Models {
    public class StoreListServiceResponse : ServiceResponse {
        [JsonProperty(PropertyName = "stores", NullValueHandling = NullValueHandling.Ignore)]
        public List<StoreModel> Result { get; set; }
    }

    public class StoreServiceResponse : ServiceResponse {
        [JsonProperty(PropertyName = "store", NullValueHandling = NullValueHandling.Ignore)]
        public StoreModel Result { get; set; }
    }

    public class ArticleListServiceResponse : ServiceResponse {
        [JsonProperty(PropertyName = "articles", NullValueHandling = NullValueHandling.Ignore)]
        public List<ArticleModel> Result { get; set; }
    }

    public class ArticleServiceResponse : ServiceResponse {
        [JsonProperty(PropertyName = "article", NullValueHandling = NullValueHandling.Ignore)]
        public ArticleModel Result { get; set; }
    }

    public class ServiceResponse {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "error_code", NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrorCode { get; set; }

        [JsonProperty(PropertyName = "error_msg", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "total_elements", NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalElements { get; set; }
    }
}