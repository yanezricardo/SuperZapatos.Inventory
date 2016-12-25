using Inventory.Site.Repositories;
using Inventory.Site.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Site.Controllers {
    public class BaseController : Controller {
        #region Fields
        private ViewModelFactory _ViewModelFactory;
        private IInventoryRepository _InventoryRepository;
        #endregion

        #region Properties
        protected IInventoryRepository TheInventoryRepository {
            get {
                return _InventoryRepository;
            }
        }

        protected ViewModelFactory TheViewModelFactory {
            get {
                if (_ViewModelFactory == null) {
                    _ViewModelFactory = new ViewModelFactory();
                }
                return _ViewModelFactory;
            }
        }
        #endregion

        #region Constructor
        public BaseController(IInventoryRepository repository) {
            _InventoryRepository = repository;
        }
        #endregion

    }
}