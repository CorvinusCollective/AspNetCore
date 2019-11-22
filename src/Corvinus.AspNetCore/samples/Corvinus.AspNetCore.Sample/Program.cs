// <copyright file="Program.cs" company="Corvinus Collective">
// Copyright (c) Corvinus Collective. All rights reserved.
// </copyright>

namespace Corvinus.AspNetCore.Sample
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Program Class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entrypoint for the application.
        /// </summary>
        /// <param name="args">A <see cref="string[]"/> containing the arguments for the application.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates an <see cref="IWebHostBuilder"/> instance.
        /// </summary>
        /// <param name="args">A <see cref="string[]"/> containing the arguments.</param>
        /// <returns>An <see cref="IWebHostBuilder"/> instance for the application.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
