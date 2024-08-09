using FluentValidation;
using SendGrid.Helpers.Errors.Model;
using System.Net.Mime;
using TaskManagementAPI.Application.Bases;
using TaskManagementAPI.Application.Features.Exceptions.AuthorizationExceptions;

namespace TaskManagementAPI.WebAPI.Middlewares.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }

    }
    private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        int statusCode = GetStatusCode(exception);
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = statusCode;

        LogException(exception);

        ExceptionModel exceptionModel = exception switch
        {
            ValidationException validationException => new ExceptionModel
            {
                Errors = validationException.Errors.Select(x => x.ErrorMessage),
                StatusCode = StatusCodes.Status400BadRequest
            },
            BusinessBaseException businessBaseException => new ExceptionModel
            {
                Errors = businessBaseException.ErrorMessages ?? new List<string> { exception.Message },
                StatusCode = StatusCodes.Status400BadRequest,
            },
            _ => new ExceptionModel
            {
                Errors = new List<string> { exception.Message, exception.InnerException?.ToString() ?? string.Empty },
                StatusCode = statusCode
            }
        };

        return httpContext.Response.WriteAsync(exceptionModel.ToString());
    }
    private int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            BusinessBaseException => StatusCodes.Status400BadRequest,
            AuthorizationException => StatusCodes.Status403Forbidden,
            AppInternalBaseException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

    private void LogException(Exception exception)
    {
        switch (exception)
        {
            case ValidationException:
                _logger.LogWarning(exception, "Validation exception occurred.");
                break;

            case BusinessBaseException:
                _logger.LogWarning(exception, "Business exception occurred.");
                break;

            case AuthorizationException:
                _logger.LogWarning(exception, "Authorization exception occurred.");
                break;

            case AppInternalBaseException:
                _logger.LogWarning(exception, "App internal exception occurred.");
                break;

            case NotFoundException:
                _logger.LogWarning(exception, "Not found exception occurred.");
                break;

            default:
                _logger.LogError(exception, "An unhandled exception occurred.");
                break;
        }
    }
}
