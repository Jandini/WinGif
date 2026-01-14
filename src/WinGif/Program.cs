// Created with Janda.Go http://github.com/Jandini/Janda.Go
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using CommandLine;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace WinGif
{
    class Program
    {
        internal static string Title { get; set; }

        static void Main(string[] args)
        {            
            try
            {
                Parser.Default.ParseArguments<Options.Capture, Options.Make, Options.Windows>(args)
                    .WithParsed((parameters) =>
                    {
                        using var provider = new ServiceCollection()
                            .AddServices()
                            .AddLogging(builder => builder
                                .AddSerilog(new LoggerConfiguration()
                                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                                    .CreateLogger(), dispose: true))
                            .BuildServiceProvider();

                        provider.LogVersion();

                        try
                        {
                            switch (parameters)
                            {
                                case Options.Capture options:
                                    var captureService = provider.GetRequiredService<ICaptureService>();

                                    Console.CancelKeyPress += (s, a) =>
                                    {
                                        captureService.StopCapture();
                                        a.Cancel = false;
                                    };
                                    captureService.StartCapture(options);
                                    break;

                                case Options.Make options:
                                    var makeService = provider.GetRequiredService<IMakeService>();
                                    makeService.Make(options);
                                    break;

                                case Options.Windows:
                                    var windowService = provider.GetRequiredService<IWindowService>();
                                    windowService.ListWindows();
                                    break;

                            };
                        }
                        catch (Exception ex)
                        {
                            provider.GetService<ILogger<Program>>()?
                                .LogCritical(ex, ex.Message);
                        }
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}