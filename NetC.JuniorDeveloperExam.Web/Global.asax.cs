using NetC.JuniorDeveloperExam.Web.Controllers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using NetC.JuniorDeveloperExam.Web.Services;
using NetC.JuniorDeveloperExam.Web.Interfaces;

namespace NetC.JuniorDeveloperExam.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityConfig.RegisterComponents();

            IBlogService blogService = new BlogService();
            blogService.GenerateCommentIds();
        }

        
    }
}
