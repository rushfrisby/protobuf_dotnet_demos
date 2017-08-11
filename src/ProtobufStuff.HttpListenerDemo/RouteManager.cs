using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProtobufStuff.HttpListenerDemo
{
    public class RouteManager
    {
        private static readonly Dictionary<string, Func<HttpListenerContext, Task<RouteResponse>>> Routes = new Dictionary<string, Func<HttpListenerContext, Task<RouteResponse>>>();

        public static bool ContainsRoute(string routeName)
        {
            routeName = routeName.Trim().ToLowerInvariant();
            return Routes.ContainsKey(routeName);
        }

        public static void AddRoute(string routeName, Func<HttpListenerContext, Task<RouteResponse>> handler)
        {
            routeName = routeName.Trim().ToLowerInvariant();

            if (Routes.ContainsKey(routeName))
            {
                throw new ApplicationException($"The route \"{routeName}\" already exists.");
            }
            Routes.Add(routeName, handler);
        }

        public static Func<HttpListenerContext, Task<RouteResponse>> GetRoute(string routeName)
        {
            routeName = routeName.Trim().ToLowerInvariant();
            return Routes[routeName];
        }
    }
}
