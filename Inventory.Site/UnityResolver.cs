using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Site {
    public class UnityResolver : IDependencyResolver {
        IUnityContainer _Container;

        public UnityResolver(IUnityContainer container) {
            if (container == null) {
                throw new ArgumentNullException("container");
            }
            _Container = container;
        }
        
        public void Dispose() {
            _Container.Dispose();
        }

        public object GetService(Type serviceType) {
            try {
                return _Container.Resolve(serviceType);
            } catch (ResolutionFailedException) {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            try {
                return _Container.ResolveAll(serviceType);
            } catch (ResolutionFailedException) {
                return new List<object>();
            }
        }
    }
}