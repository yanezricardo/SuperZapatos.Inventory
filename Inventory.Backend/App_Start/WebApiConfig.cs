using Inventory.Backend.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Inventory.Backend {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            var container = new UnityContainer();
            container.RegisterType<IInventoryRepository, InventoryRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            config.MessageHandlers.Add(new ResponseHandler());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Services",
                routeTemplate: "services",
                defaults: new { controller = "Services" }
            );
        }
    }
}
