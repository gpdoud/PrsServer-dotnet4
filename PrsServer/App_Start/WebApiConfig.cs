using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace PrsServer {
	public static class WebApiConfig {
		public static void Register(HttpConfiguration config) {
			// Web API configuration and services
			config.EnableCors();

			// install-package Microsoft.AspNet.WebApi.Cors

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "Authentication",
				routeTemplate: "{controller}/{action}/{username}/{password}",
				defaults: new { }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultWebApi",
				routeTemplate: "{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Formatters.Remove(config.Formatters.XmlFormatter);
		}
	}
}
