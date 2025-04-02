using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using PD_FOOD.Middlewares;

namespace PD_FOOD.Test.Middleware
{
    public class ExceptionMiddlewareTest
    {
        [Fact]
        public async Task Should_Return_500_When_Unhandled_Exception_Occurs()
        {
            var loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            var middleware = new ExceptionMiddleware(
                async (context) => throw new Exception("Simulated failure"),
                loggerMock.Object
            );

            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            await middleware.Invoke(context);

            Assert.Equal(500, context.Response.StatusCode);
            responseStream.Position = 0;
            var responseText = new StreamReader(responseStream).ReadToEnd();
            Assert.Contains("internal server error", responseText.ToLower());
        }

        [Fact]
        public async Task Should_Propagate_StatusCode_400()
        {
            var loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            var middleware = new ExceptionMiddleware(async (context) =>
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad Request");
            }, loggerMock.Object);

            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            await middleware.Invoke(context);

            Assert.Equal(400, context.Response.StatusCode);
        }

        [Fact]
        public async Task Should_Propagate_StatusCode_403()
        {
            var loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
            var middleware = new ExceptionMiddleware(async (context) =>
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden");
            }, loggerMock.Object);

            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            await middleware.Invoke(context);

            Assert.Equal(403, context.Response.StatusCode);
        }
    }
}
