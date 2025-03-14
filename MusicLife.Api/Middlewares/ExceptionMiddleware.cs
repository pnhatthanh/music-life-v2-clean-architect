using Microsoft.AspNetCore.Http;
using MusicLife.Application.Exceptions;
using MusicLife.Domain.Enums;
using System.Text.Json;

namespace MusicLife.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(HttpException ex)
            {
                context.Response.StatusCode =(int) ex.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(context.Response));
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    statusCode = StatusCodes.Status500InternalServerError,
                    code = ExceptionEnum.Internal,
                    message = "An unexpected error occured on the server"
                }));
            }
        }
    }
}
