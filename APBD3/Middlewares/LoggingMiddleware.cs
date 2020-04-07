using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next) { _next = next; }
        public async Task InvokeAsync(HttpContext httpContext)
        { //Our code 

            httpContext.Request.EnableBuffering();

            if (httpContext.Request != null)
            {
                var method = httpContext.Request.Method.ToString();
                var query = httpContext.Request.Query.ToString();
                var body = String.Empty;
                var path = httpContext.Request.Path.ToString();

                using (var bodyReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await bodyReader.ReadToEndAsync();
                }

                var log = "\n" + DateTime.Now.ToString() + "\n" +
                    "METHOD: " + method + "\n" +
                    "PATH: " + path + "\n" +
                    "BODY: " + body + "\n" +
                    "QUERY: " + query;
                File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "log.txt"), log);
            }

            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next(httpContext); 
        }

    }
}
