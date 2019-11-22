// <copyright file="ConfigurationExtensions.cs" company="Corvinus Collective">
// Copyright (c) Corvinus Collective. All rights reserved.
// </copyright>

namespace Corvinus.AspNetCore.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Configuration Extensions Class.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the IConfiguration from the specified json folder.
        /// </summary>
        /// <param name="jsonFileName">A string containing the json file name.</param>
        /// <param name="basePath">The base path of the json file. If base path is left null then it will look in the base directory of the AppContext.</param>
        /// <returns>An IConfiguration.</returns>
        public static IConfiguration GetConfiguration(string jsonFileName, string basePath = null)
        {
            if (basePath == null)
            {
                basePath = AppContext.BaseDirectory;
            }

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.Configure(basePath, jsonFileName);
            return builder.Build();
        }

        private static IConfigurationBuilder Configure(this IConfigurationBuilder builder, string basePath, string jsonFileName)
        {
            return builder
                .SetBasePath(basePath)
                .AddJsonFile(jsonFileName, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
