using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Statmath.Application.Client.Common.Abstraction;
using Statmath.Application.Client.Common.Implementation;
using System;
using System.Threading.Tasks;

namespace Statmath.Application.Client
{
    internal class Program
    {
        private static AppSettings _appSettings = new AppSettings();
        private static IConfiguration _configuration;

        private static IServiceProvider _serviceProvider;

        private static async Task Main(string[] args)
        {
            // beware the order of execution
            // initialize configuration
            SetConfiguration();
            // set up the appsettings to address correctly
            SetAppSettings();
            // register necessaray objects and relations 
            RegisterServices();

            // create scope of service provider
            var scope = _serviceProvider.CreateScope();
            // aquire main console app class -> run to proceed
            await scope.ServiceProvider.GetService<ConsoleApplication>().RunAsync();

            // free memory
            DisposeServices();

            //// instantiate necessery classes
            //planConverter = new PlanConverter();
            //csvHelper = new CsvHelper(planConverter);

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //configuration = builder.Build();
            //ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);
        }

        private static void SetAppSettings()
        {
            _configuration.GetSection(nameof(AppSettings)).Bind(_appSettings);
        }

        //private static void InitializeApplicationSettings()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        //    configuration = builder.Build();
        //    ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);
        //}

        private static void SetConfiguration()
        {
            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                //.AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        private static void RegisterServices()
        {

            var services = new ServiceCollection();
            services.AddSingleton(typeof(AppSettings), _appSettings);
            services.AddSingleton<IConnectionHandler, ConnectionHandler>();
            services.AddSingleton<ConsoleApplication>();
            _serviceProvider = services.BuildServiceProvider();
        }


        //private static void Runner()
        //{
        //    do
        //    {
        //        Console.Write(consolePrefix);
        //        var userInput = Console.ReadLine().TrimStart().TrimEnd();
        //        var command = userInput.Split(' ')[0];

        //        switch (command)
        //        {
        //            case "":
        //                Console.WriteLine("Enter command pls...");
        //                break;
        //            case "--help":
        //                WriteHelpMenu();
        //                break;
        //            case "upload" when userInput.Split(' ').Length == 2:
        //                break;
        //            case "readall":
        //                break;
        //            case "read" when userInput.Split(' ').Length == 3:
        //                break;
        //            case "exit":
        //                //Environment.Exit(0);
        //                isActive = false;
        //                break;
        //            case "clr":
        //                Console.Clear();
        //                break;
        //            default:
        //                Console.WriteLine($"\"{command}\" is a unknown command. For more information enter \"--help\"");
        //                break;
        //        }
        //    } while (isActive);
        //}

        //private static void Upload(string filePath)
        //{
        //    // 
        //}

        //private static void WriteHelpMenu()
        //{
        //    Console.WriteLine("\"upload <path>\"        --> get data from csv and upload it to database");
        //    Console.WriteLine("\"readall\"              --> read all data from database and print it to console");
        //    Console.WriteLine("\"read <status> <day>\"  --> exp. read end 01-01-2000");
        //    Console.WriteLine("\"clr\"                  --> clear console window");
        //    Console.WriteLine("\"exit\"                 --> exit the programm");
        //}

        //private static string GetApiUrl()
        //    => new UriBuilder
        //    {
        //        Scheme = appSettings.Scheme,
        //        Host = appSettings.Host,
        //        Port = appSettings.Port,
        //        Path = appSettings.Path
        //    }.ToString();


        /// <summary>
        /// Perform disposing to free resources
        /// </summary>
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
                return;
            if (_serviceProvider is IDisposable)
                (_serviceProvider as IDisposable).Dispose();
        }
    }
}