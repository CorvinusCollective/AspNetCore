// <copyright file="ExceptionMiddleware.cs" company="Corvinus Collective">
// Copyright (c) Corvinus Collective. All rights reserved.
// </copyright>

namespace Corvinus.AspNetCore.Exception
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// HttpStatusCodeExceptionMiddleware Class.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">Throws exception if value is null on creation.</exception>
        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        /// <summary>
        /// Invokes the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    _logger.LogError("The response has already started, the http status code middleware will not be executed.", ex);
                    return;
                }

                _logger.LogDebug($"Unhandled Exception: {ex.Message}", ex);
                var jsonValue = JsonConvert.SerializeObject(ex, Formatting.Indented);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = @"application/json";
                httpContext.Response.Headers.Add("exception", "generalException");
                await httpContext.Response.WriteAsync(jsonValue);

                return;
            }
        }
    }
}
