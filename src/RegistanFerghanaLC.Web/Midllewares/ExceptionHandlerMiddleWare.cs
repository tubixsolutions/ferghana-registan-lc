using Newtonsoft.Json;
using RegistanFerghanaLC.Web.Exceptions;
using RegistanFerghanaLC.Web.Models;
using System.Text.Json.Serialization;

namespace RegistanFerghanaLC.Web.Midllewares
{
    public class ExceptionHandlerMiddleWare
    {
        private RequestDelegate _next;

        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (StatusCodeException exception)
            {
                await UserErrorHandlerAsync(exception, context);
            }
            catch (Exception ex)
            {
                await ServiceErrorHandlerAsync(ex, context);
            }

        }
        public async Task UserErrorHandlerAsync(StatusCodeException exception, HttpContext context)
        {
            context.Response.ContentType= "application/json";
            ErrorDto dto = new ErrorDto()
            {
                StatusCode = (int)exception.StatusCode,
                Message = exception.Message
            };
            string jsonData = JsonConvert.SerializeObject(dto);
            context.Response.StatusCode = (int)exception.StatusCode;
            await context.Response.WriteAsync(jsonData);
        }
        public async Task ServiceErrorHandlerAsync(Exception exception, HttpContext context)
        {
            ErrorDto dto = new()
            {
                Message = exception.Message,
                StatusCode = 500
            };
            string jsonData = JsonConvert.SerializeObject(dto);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(jsonData);
        }
    }
}
