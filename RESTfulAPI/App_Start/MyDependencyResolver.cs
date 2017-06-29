using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace RESTfulAPI
{
    internal class MyDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _container;

        public MyDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new MyDependencyResolver(child);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}