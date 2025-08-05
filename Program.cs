using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netwise.Interfaces;
using Netwise.Models;
using Netwise.Services;
using Netwise.View;
using Netwise.Controllers;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("D:\\Własne Projekty\\RecruitmentTask\\Netwise\\appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var catFactConfig = configuration.GetSection("CatFact").Get<CatFactConfig>();

        if (catFactConfig == null)
        {
            throw new ArgumentNullException(nameof(catFactConfig), "CatFact configuration section is missing or invalid.");
        }

        var serviceProvider = new ServiceCollection()
            .AddSingleton(catFactConfig)
            .AddSingleton<HttpClient>()
            .AddSingleton<IService, FactService>()
            .AddSingleton<TxtFileHandler>(provider =>
            {
                var config = provider.GetRequiredService<CatFactConfig>();
                return new TxtFileHandler(config.TxtFilePath);
            })
            .AddSingleton<MainView>()
            .AddSingleton<MainController>()
            .BuildServiceProvider();

        var controller = serviceProvider.GetRequiredService<MainController>();
        await controller.RunAsync();
    }
}