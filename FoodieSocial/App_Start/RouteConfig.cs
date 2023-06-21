using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodieSocial
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "PostLike",
            //    url: "Home/Like",
            //    defaults: new { controller = "Home", action = "Like" }
            //);

            routes.MapRoute(
                name: "FriendRequests",
                url: "Friend/FriendRequests",
                defaults: new { controller = "Friend", action = "FriendRequests" }
            );
        }
        public static void Register(RouteCollection routes)
        {
            RegisterRoutes(routes);
        }
    }
}