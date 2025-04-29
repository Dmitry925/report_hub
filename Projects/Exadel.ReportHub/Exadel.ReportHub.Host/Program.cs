using System.Diagnostics.CodeAnalysis;

namespace Exadel.ReportHub.Host;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static async Task Main(string[] args)
    {
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);

        var host = CreateHostBuilder(args).Build();

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
