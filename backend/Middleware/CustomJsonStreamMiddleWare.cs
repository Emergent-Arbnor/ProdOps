using backend.Streams;
using System.Text.Json; // For JsonException
using Microsoft.AspNetCore.Mvc; // For ProblemDetails

namespace backend.Middleware;

/// <summary>
/// Middleware that wraps the HTTP request body stream to intercept JSON data.
/// Ensures that no duplicate keys are present in the JSON payload.
/// </summary>
public class CustomJsonStreamMiddleware
{
    
    private readonly RequestDelegate _next;

    public CustomJsonStreamMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // The custom json stream should run only if there is a json in the request
        if (context.Request.ContentType?.Contains("application/json") == true)
        {
            Stream originalStream = context.Request.Body;

            // Wrap the original stream with CustomJsonStream
            context.Request.Body = new CustomJsonStream(originalStream);
            await _next(context); // Continue to the next middleware/controller
        
            // Restore the original stream to prevent breaking other middleware
            context.Request.Body = originalStream;
        }
        else
        {
            await _next(context);
        }
    }
}