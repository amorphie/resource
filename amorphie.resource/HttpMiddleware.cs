using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IO;

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
                RecyclableMemoryStreamManager _recyclableMemoryStreamManager = new();
                await using var requestStream = _recyclableMemoryStreamManager.GetStream();

                await context.Request.Body.CopyToAsync(requestStream);
                var body = ReadStreamInChunks(requestStream);
                context.Request.Body.Position = 0;

                Console.WriteLine("Http Request Body : " + body);
                Console.WriteLine("----Headers----");
                foreach (var h in context.Request.Headers)
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

        private string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

    }
}