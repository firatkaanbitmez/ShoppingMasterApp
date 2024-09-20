using Serilog;
using System.Text;

namespace ShoppingMasterApp.API.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the request
            await LogRequest(context);

            // Log the response
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();  // Allow multiple reads of the request stream

            var request = context.Request;
            var method = request.Method;
            var path = request.Path;
            var queryString = request.QueryString.ToString();
            var headers = request.Headers;
            var body = "";

            // Read request body
            if (request.Body.CanSeek)
            {
                request.Body.Position = 0;
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    body = await reader.ReadToEndAsync();
                    request.Body.Position = 0;  // Reset the stream position
                }
            }
            Log.Information("LoginAdminCommand received with Email:");
            Console.WriteLine("LoginAdminCommand received with}");
            _logger.LogInformation($"Incoming Request: Method: {method}, Path: {path}, QueryString: {queryString}, Headers: {headers}, Body: {body}");
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);  // Call the next middleware

                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var statusCode = context.Response.StatusCode;
                _logger.LogInformation($"Outgoing Response: StatusCode: {statusCode}, Body: {text}");

                await responseBody.CopyToAsync(originalBodyStream);  // Copy back the response body
            }
        }
    }
}
