using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Microsoft.Practices.Unity;

using DAL;
using Services;
using Repository;
using Domain;

namespace RESTfulAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //Unity container configuration
            var container = new UnityContainer();
            RegisterUnityServices(container);
            config.DependencyResolver = new MyDependencyResolver(container);

            #region Serializer Configuration
            //Setting up the JSON serializing format
            //and removing the XML serializer            

            //Remove a formatter
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Set JSON Formetter indented
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //Set Camel Case style to properties 
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            #endregion

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void RegisterUnityServices(UnityContainer container)
        {
            //Resolving services 
            container.RegisterType<IDepartmentService, DepartmentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeService, EmployeeService>(new HierarchicalLifetimeManager());

            //Resolving repositories
            container.RegisterType<IRepository<Department>, Repository<Department>>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepository<Employee>, Repository<Employee>>(new HierarchicalLifetimeManager());

            //Resolving the DB context
            container.RegisterType<IWebDBContext, OACompanyEntities>(new HierarchicalLifetimeManager());

        }
    }
}
