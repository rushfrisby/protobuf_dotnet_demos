using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProtoBuf;

namespace ProtobufStuff.NetCoreDemo
{
    public class Startup
    {
        private IDictionary<string, Func<HttpContext, Task>> _handlers;

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _handlers = new Dictionary<string, Func<HttpContext, Task>>
            {
                {"/Now", TestAsync}
            };

            app.Run(async context =>
            {
                if (_handlers.ContainsKey(context.Request.Path))
                {
                    await _handlers[context.Request.Path](context);
                }
                else
                {
                    await NotFoundHandlerAsync(context);
                }
            });
        }

        private static async Task TestAsync(HttpContext context)
        {
            context.Response.ContentType = "application/x-protobuf";

            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, new Now());
                await context.Response.Body.WriteAsync(ms.ToArray(), 0, (int)ms.Length);
            }
        }

        private static async Task NotFoundHandlerAsync(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync("Not Found");
        }
    }
}
