using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProtobufStuff.Common.Serializers;

namespace ProtobufStuff.HttpListenerDemo
{
    internal class WebServer
    {
        private readonly HttpListener _listener;
        private const int MaxConnections = 64;

        public WebServer()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:5000/");
        }

        public void Start()
        {
            _listener.Start();

            Task.Run(async () =>
            {
                var sem = new SemaphoreSlim(MaxConnections);

                while (_listener.IsListening)
                {
                    await sem.WaitAsync();
                    await _listener.GetContextAsync().ContinueWith(async t =>
                    {
                        sem.Release();
                        var context = await t;
                        try
                        {
                            await HandleRequest(context);
                        }
                        catch (Exception ex)
                        {
                            await WriteInternalServerError(ex.Message, context);
                        }
                        context?.Response.OutputStream?.Close();
                    });
                }
            });
        }

        private static async Task HandleRequest(HttpListenerContext context)
        {
            if (!RouteManager.ContainsRoute(context.Request.RawUrl))
            {
                await WriteNotFoundResponseAsync(context);
                return;
            }

            var route = RouteManager.GetRoute(context.Request.RawUrl);
            RouteResponse response;

            try
            {
                response = await route(context);
            }
            catch (Exception ex)
            {
                await WriteInternalServerError(ex.Message, context);
                return;
            }

            await WriteOkResponseAsync(context, response);
        }

        private static async Task WriteOkResponseAsync(HttpListenerContext context, RouteResponse response)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.StatusDescription = "OK";
            context.Response.ContentType = response.ContentType;
            context.Response.ContentLength64 = response.Content.Length;
            await context.Response.OutputStream.WriteAsync(response.Content, 0, response.Content.Length);
        }

        private static async Task WriteInternalServerError(string message, HttpListenerContext context)
        {
            const string serverError = "Internal Server Error";
            if (string.IsNullOrWhiteSpace(message))
            {
                message = serverError;
            }
            var data = Encoding.UTF8.GetBytes(message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusDescription = serverError;
            context.Response.ContentType = ContentTypes.PlainText;
            context.Response.ContentLength64 = data.Length;
            await context.Response.OutputStream.WriteAsync(data, 0, data.Length);
        }

        private static async Task WriteNotFoundResponseAsync(HttpListenerContext context)
        {
            const string message = "Not Found";
            var notFound = Encoding.UTF8.GetBytes(message);
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.StatusDescription = message;
            context.Response.ContentType = ContentTypes.PlainText;
            context.Response.ContentLength64 = notFound.Length;
            await context.Response.OutputStream.WriteAsync(notFound, 0, notFound.Length);
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
