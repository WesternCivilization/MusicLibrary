using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using MusicLibrary.Mapping;

namespace MusicLibrary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoC.Initialize();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //View model mappings
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AlbumProfile>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
