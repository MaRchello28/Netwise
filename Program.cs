using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netwise.Interfaces;
using Netwise.Models;
using Netwise.Services;
using Netwise.View;
using Netwise.Controllers;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("D:\\Własne Projekty\\RecruitmentTask\\Netwise\\appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var catFactConfig = configuration.GetSection("CatFact").Get<CatFactConfig>();

        if(catFactConfig == null)
        {
            throw new ArgumentNullException();
        }

        var serviceProvider = new ServiceCollection()
            .AddSingleton(catFactConfig)
            .AddSingleton<HttpClient>()
            .AddSingleton<IService, FactService>()
            .AddSingleton<TxtFileHandler>()
            .BuildServiceProvider();

        var service = serviceProvider.GetRequiredService<IService>();
        var fileHandler = new TxtFileHandler(catFactConfig.TxtFilePath);

        var view = new MainView();
        var Controller = new MainController(service, fileHandler, view);

        await Controller.RunAsync();
    }
}