using System.Net;
using System.Threading.Tasks;
using ProtobufStuff.Common.Entities;
using ProtobufStuff.Common.Serializers;

namespace ProtobufStuff.HttpListenerDemo
{
    internal class RouteHandlers
    {
        public static async Task<RouteResponse> Now(HttpListenerContext context)
        {
            return await GetRouteResponse(context, new Now());
        }

        private static async Task<RouteResponse> GetRouteResponse<T>(HttpListenerContext context, T data)
        {
            var response = new RouteResponse(context.Request.ContentType);
            var serializer = ContentTypes.GetSerializer(context.Request.ContentType);
            response.Content = await serializer.SerializeAsync(data);
            return response;
        }
    }
}
