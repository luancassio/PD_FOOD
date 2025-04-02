namespace PD_FOOD.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // executa o restante da pipeline
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    status = 500,
                    message = "An internal server error occurred.",
                    detail = ex.Message // pode remover isso em produção
                };

                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }

}
