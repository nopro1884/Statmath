using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Statmath.Application.Client.Commands.Abstraction;
using Statmath.Application.Client.Commands.Implementation;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Client.Handler.Implementation;
using System;

namespace Statmath.Application.Client
{
    public class ApplicationSetup : IDisposable
    {
        private readonly AppSettings _appSettings = new AppSettings();
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider;

        public ApplicationSetup()
        {
            _configuration = GetConfiguration();
            _configuration.GetSection(nameof(AppSettings)).Bind(_appSettings);
            _serviceProvider = GetServiceProvider();
        }

        public ConsoleApplication GetAppContext()
        {
            // create scope of service provider
            var scope = _serviceProvider.CreateScope();
            // aquire main console app class
            return scope.ServiceProvider.GetService<ConsoleApplication>();
        }

        private  IConfiguration GetConfiguration()
        {
            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                //.AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private  IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            // provide app settings
            services.AddSingleton(typeof(AppSettings), _appSettings);
            // provide commands
            services.AddSingleton<ICreateCommand, CreateCommand>();
            services.AddSingleton<IReadCommand, ReadCommand>();
            services.AddSingleton<IExitCommand, ExitCommand>();
            services.AddSingleton<IHelpCommand, HelpCommand>();
            // provide handler
            services.AddSingleton<ICommandHandler, CommandHandler>();
            services.AddSingleton<IConnectionHandler, ConnectionHandler>();
            services.AddSingleton<ConsoleApplication>();
            return services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (_serviceProvider == null)
                return;
            if (_serviceProvider is IDisposable)
                (_serviceProvider as IDisposable).Dispose();
        }
    }
}
