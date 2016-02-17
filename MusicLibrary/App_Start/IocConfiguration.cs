using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MusicLibrary.DataAccess;
using MusicLibrary.Services;

namespace MusicLibrary
{

    public class IoC
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            //Configure for MVC
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //Register repository
            builder.RegisterAssemblyTypes(typeof(ArtistRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            //Register services
            builder.RegisterAssemblyTypes(typeof(PlaylistService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            //Configure database
            builder.Register<IDbConnection>(c =>
            {
                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

                return conn;

            }).InstancePerRequest();


            //Create the container
            var container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}