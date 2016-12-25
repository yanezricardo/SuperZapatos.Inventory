using Inventory.Backend.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Backend.Models {
    [JsonObject(Title = "store")]
    [JsonPluralName("stores")]
    public class StoreModel {
        [JsonProperty(PropertyName = "id")]
        public int StoreID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}