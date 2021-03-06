﻿using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Beetle.Tests.Integration {

    public class MvcApplication : HttpApplication {

        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
