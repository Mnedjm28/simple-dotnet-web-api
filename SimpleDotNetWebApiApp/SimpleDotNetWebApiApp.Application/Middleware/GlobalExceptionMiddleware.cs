using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleDotNetWebApiApp.Infrastructure.Exceptions;
using System.Net;
using System.Text.Json;

namespace SimpleDotNetWebApiApp.Application.Middleware
{
    public class GlobalExceptionMiddleware(RequestDelegate _next, ILogger<GlobalExceptionMiddleware> _logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleProblemAsync(context, HttpStatusCode.Unauthorized, "Unauthorized", ex);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleProblemAsync(context, HttpStatusCode.NotFound, "Resource not found", ex);
            }
            catch (NotFoundException ex)
            {
                await HandleProblemAsync(context, HttpStatusCode.NotFound, "Record not found", ex);
            }
            catch (DbUpdateException ex)
            {
                var failedEntries = ex.Entries;
                foreach (var entry in failedEntries)
                {
                    var entityName = entry.Metadata.Name;
                    var properties = entry.Properties.Where(p => p.IsModified && !p.IsTemporary);
                    foreach (var property in properties)
                    {
                        var propertyName = property.Metadata.Name;
                        Console.WriteLine($"Failed to update field: {propertyName} in entity: {entityName}");
                    }
                }
            }
            catch (Exception ex)
            {
                var tt = ex.GetType();
                await HandleProblemAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.", ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName.Split('.')[1])
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var response = new { errors };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private async Task HandleProblemAsync(HttpContext context, HttpStatusCode status, string message, Exception ex)
        {
            _logger.LogError(ex, message);

            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Title = message,
                Status = (int)status,
                Detail = ex.Message,
                Instance = context.Request.Path,
                Type = $"https://httpstatuses.com/{(int)status}"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
