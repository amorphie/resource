using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amorphie.resource
{
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                string requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                context.Request.Body.Position = 0;
                Console.WriteLine("Http Request Body : "+requestBody);
                Console.WriteLine("----Headers----");
                foreach(var h in context.Request.Headers)
                {
                    Console.WriteLine($"  Key:{h.Key} || Val:{h.Value}");
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            

        }
           
    }
}