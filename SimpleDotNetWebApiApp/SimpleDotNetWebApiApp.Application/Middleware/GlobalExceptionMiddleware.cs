using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleDotNetWebApiApp.Application.Helpers;
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
                await HandleDbUpdateAsync(context, HttpStatusCode.BadRequest, "Perform action", ex);
            }
            catch (Exception ex)
            {
                var type = ex.GetType();
                await HandleProblemAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.", ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
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

        private async Task HandleDbUpdateAsync(HttpContext context, HttpStatusCode status, string message, DbUpdateException ex)
        {
            object responseDetails = new ProblemDetails
            {
                Title = message,
                Status = (int)status,
                Detail = ex.Message,
                Instance = context.Request.Path,
                Type = $"https://httpstatuses.com/{(int)status}"
            };

            var failedEntries = ex.Entries;
            if (failedEntries.Count == 1)
            {
                var entry = failedEntries[0];
                var entityName = entry.Metadata.Name;
                var properties = entry.Properties.Where(p => !p.IsModified && !p.IsTemporary);
                foreach (var property in properties)
                {
                    var propertyName = property.Metadata.Name;
                    if (ex.InnerException.ToString().Contains(propertyName))

                        responseDetails = new ProblemDetails
                        {
                            Title = message,
                            Status = (int)status,
                            Detail = $"{propertyName} with key '{Utils.GetPropertyValue(entry.Entity, propertyName)} was not found.",
                            Instance = context.Request.Path,
                            Type = $"https://httpstatuses.com/{(int)status}"
                        };
                }
                _logger.LogError(ex, string.Join(",\n", ((ProblemDetails)responseDetails).Detail));
            }
            else
            {
                var problemDetails = new List<ProblemDetails>();
                foreach (var entry in failedEntries)
                {
                    var entityName = entry.Metadata.Name;
                    foreach (var property in entry.Properties)
                    {
                        var propertyName = property.Metadata.Name;
                        if (ex.InnerException.ToString().Contains($"_{propertyName}"))
                            problemDetails.Add(new ProblemDetails
                            {
                                Title = message,
                                Status = (int)status,
                                Detail = $"{propertyName} with key '{Utils.GetPropertyValue(entry.Entity, propertyName)} was not found.",
                                Instance = context.Request.Path,
                                Type = $"https://httpstatuses.com/{(int)status}"
                            });
                    }
                }
                _logger.LogError(ex, string.Join(",\n", problemDetails.Select(o => o.Detail)));
                responseDetails = problemDetails;
            }

            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(responseDetails));
        }
    }
}
