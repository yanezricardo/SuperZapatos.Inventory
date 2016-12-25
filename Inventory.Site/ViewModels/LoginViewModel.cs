using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Site.ViewModels {
    public class LoginViewModel {
        [Required]
        [Description("Usuario")]
        public string UserName { get; set; }

        [Required]
        [Description("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}