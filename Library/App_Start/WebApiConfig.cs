using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Library
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var setings = config.Formatters.JsonFormatter.SerializerSettings;
            setings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            setings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
