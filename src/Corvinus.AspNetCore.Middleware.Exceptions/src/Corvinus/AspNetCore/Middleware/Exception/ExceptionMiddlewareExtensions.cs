// <copyright file="ExceptionMiddlewareExtensions.cs" company="Corvinus Collective">
// Copyright (c) Corvinus Collective. All rights reserved.
// </copyright>

namespace Corvinus.AspNetCore.Middleware.Exception
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ExceptionMiddlewareExtensions Class.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Uses the HTTP status code exception middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="logger">The Logger.</param>
        /// <returns><see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder, ILogger logger)
        {
            return builder.UseMiddleware<ExceptionMiddleware>(logger);
        }
    }
}
