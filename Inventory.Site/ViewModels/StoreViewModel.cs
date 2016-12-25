using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Inventory.Site.Models;

namespace Inventory.Site.ViewModels {
    public class StoreViewModel {
        public int StoreID { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo nombre es requerido.")]
        public string Name { get; set; }

        [DisplayName("Dirección")]
        [Required(ErrorMessage = "El campo dirección es requerido.")]
        public string Address { get; set; }

        [DisplayName("Artículos")]
        public List<ArticleViewModel> Articles { get; set; }
    }
}