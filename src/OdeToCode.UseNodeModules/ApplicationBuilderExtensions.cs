﻿using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures and adds static file middleware to serve files directly from the node_modules
        /// folder in this project
        /// </summary>
        /// <param name="app">The IApplicationBuilder object</param>
        /// <param name="environment">The IHostingEnvironment object</param>
        /// <returns>The IApplicationBuilder object</returns>
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, 
                                                        IHostingEnvironment environment)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (environment == null) throw new ArgumentNullException(nameof(environment));

            AddMiddleware(app, environment);

            return app;
        }

        private static void AddMiddleware(IApplicationBuilder app, IHostingEnvironment environment)
        {
            var path = Path.Combine(environment.ContentRootPath, "node_modules");
            var provider = new PhysicalFileProvider(path);

            var options = new FileServerOptions {RequestPath = "/node_modules"};
            options.StaticFileOptions.FileProvider = provider;
            options.EnableDirectoryBrowsing = false;
            app.UseFileServer(options);
        }
    }
}