using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dani551.Open.FileService
{
    public static class ServiceExtension
    {

        public static void AddFileService(this IServiceCollection services,
            Action<FileServiceConfig> setupAction)
        {
            // Add the service.
            services.AddScoped<IFileService, FileService>();

            // configure service
            services.Configure(setupAction);
        }
    }
}
