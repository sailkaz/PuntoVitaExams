using PuntoVitaExams.API.Exceptions;

namespace PuntoVitaExams.API.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException NFException)
            {
                _logger.LogError(NFException.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(NFException.Message);
            }
            catch (ForbidException FException)
            {
                _logger.LogError(FException.Message);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(FException.Message);
            }
            catch (BadRequestException BRException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(BRException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
