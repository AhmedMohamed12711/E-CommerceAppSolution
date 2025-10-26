using DomainLayer.Exceptions;
using Shared.ErrorModel;
using System.Text.Json;

namespace E_CommerceApp.CustomMiddleware
{
    public class CustomExceptionHandelerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandelerMiddleware> _logger;

        public CustomExceptionHandelerMiddleware(RequestDelegate Next, ILogger<CustomExceptionHandelerMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPoint(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SomeThing Went Wrong");
                //Set Status Code For Response
                await HandleExceptionsAsync(httpContext, ex);
            }


        }

        private static async Task HandleExceptionsAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundExceptions => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            //Set Content Type For Response
            httpContext.Response.ContentType = "application/json";
            //Response Object
            var Response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            //Return Object As Json
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static async Task HandleNotFoundEndPoint(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The End Point{httpContext.Request.Path} Is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}