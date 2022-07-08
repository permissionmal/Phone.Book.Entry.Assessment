using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhoneBook.API.Services
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                if (error is HttpStatusException status)
                {
                    Log.Error($"Something went wrong: {error}");
                    response.StatusCode = (int)status.StatusCode;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Log.Error($"Something went wrong: {error}");
                }

                var result = JsonSerializer.Serialize(new ErrorDetails
                {
                    StatusCode = response.StatusCode,
                    Message = error.Message
                });

                await response.WriteAsync(result);
            }
        }
    }

    public class HttpStatusException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public HttpStatusException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}