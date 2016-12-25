using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Backend.Entities {
    public class Store {
        public int StoreID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}