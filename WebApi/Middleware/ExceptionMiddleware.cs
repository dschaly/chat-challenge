using Domain.DTOs;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;

namespace WebApi.Middleware
{
    [Serializable()]
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            httpContext.Request.EnableBuffering();
            var body = string.Empty;

            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(
                httpContext.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                httpContext.Request.Body.Position = 0;
            }

            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, body, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string body, Exception exception)
        {
            var logModel = new LogModel
            {
                Application = "Code Challenge - Davidson Schaly",
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                RunningTime = DateTime.Now.ToString("HH:mm:ss"),
                Scenario = context.Features.Get<IHttpRequestFeature>().Path,
                Content = body
            };

            if (exception is InvalidOperationException)
            {
                logModel.Type = "WARN";
                logModel.Information = exception.Message;
                _logger.LogWarning(JsonConvert.SerializeObject(logModel));
            }
            else
            {
                logModel.Type = "ERROR";
                logModel.Information = exception.Message;
                logModel.Exception = exception.InnerException != null ? exception.InnerException.ToString() : string.Empty;
                logModel.Etc = !string.IsNullOrEmpty(exception.StackTrace) ? exception.StackTrace : string.Empty;
                _logger.LogError(exception, JsonConvert.SerializeObject(logModel));
            }

            var result = JsonConvert.SerializeObject(new { message = GetUsableException(exception).Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(result);
        }

        protected Exception? GetUsableException(Exception err)
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
