using Microsoft.Extensions.DependencyInjection;
using DotNetOpen.Services.SmsService;
using System;

namespace DotNetOpen.Services.SmsService
{
    public static class ServiceExtension
    {
        public static void AddSmsService(this IServiceCollection services,
            Action<ISmsServiceConfig> setupAction)
        {
            // Add the service.
            services.AddSingleton<ISmsService, SmsService>();

            // configure service
            services.Configure(setupAction);
        }
    }
}
