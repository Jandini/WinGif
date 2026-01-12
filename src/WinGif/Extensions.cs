using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace WinGif
{
    internal static class Extensions
    {


        internal static IServiceProvider LogVersion(this IServiceProvider provider)
        {
            var version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            Program.Title = $"WinGif {version} by Matt Janda";
            Console.Title = Program.Title;

            provider
                .GetRequiredService<ILogger<Program>>()
                .LogInformation("WinGif {version}", version);

            return provider;
        }

        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<ICaptureService, CaptureService>()
                .AddTransient<IMakeService, MakeService>()
                .AddTransient<IWindowService, WindowService>();
        }
    }
}
