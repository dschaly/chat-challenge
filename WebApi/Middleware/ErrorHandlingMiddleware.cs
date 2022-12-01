using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;

namespace WebApi.Middleware
{
    [Serializable()]
    public class EsbException : Exception
    {
        public EsbException(HttpStatusCode httpStatusCode, string httpResponse)
        {
            HttpStatusCode = httpStatusCode;
            HttpResponse = httpResponse;
        }

        protected EsbException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public HttpStatusCode HttpStatusCode { get; set; }
        public string HttpResponse { get; set; }
    }

    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                string message = $"Unexpected error: {ex}";
                _logger.LogError(message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            string result;

            if (exception.GetType() == typeof(EsbException))
            {
                code = ((EsbException)exception).HttpStatusCode;
                result = ((EsbException)exception).HttpResponse;
            }
            else
            {
                var exceptionReturn = GetUsableException(exception);
                result = exceptionReturn is not null ? exception.Message : string.Empty;
            }

            result = JsonConvert.SerializeObject(new { message = result });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Exception? GetUsableException(Exception err)
        {
            if (err is TargetInvocationException)
            {
                if (err.InnerException is not null)
                {
                    return GetUsableException(err.InnerException);
                }

                return null;
            }
            else
            {
                return err;
            }
        }
    }
}
