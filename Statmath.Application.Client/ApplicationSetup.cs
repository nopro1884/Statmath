using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Statmath.Application.Client.Commands.Abstraction;
using Statmath.Application.Client.Commands.Implementation;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Client.Handler.Implementation;
using Statmath.Application.DataHelper.Abstraction;
using Statmath.Application.DataHelper.Implementation;
using System;

namespace Statmath.Application.Client
{
    public class ApplicationSetup : IDisposable
    {
        private readonly AppSettings _appSettings = new AppSettings();
        private IConfiguration _configuration;
        private IServiceScope _serviceScope;
        private IServiceProvider _serviceProvider;

        public ApplicationSetup()
        {
            _configuration = GetConfiguration();
            _configuration.GetSection(nameof(AppSettings)).Bind(_appSettings);
            _serviceProvider = GetServiceProvider();
            _serviceScope = _serviceProvider.CreateScope();
        }

        public ConsoleApplication GetAppContext()
        {
            // aquire main console app class
            return _serviceScope.ServiceProvider.GetService<ConsoleApplication>();
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
            // common app stuff
            services.AddSingleton<IPlanConverter, PlanConverter>();
            services.AddSingleton<ICsvHelper, CsvHelper>();
            // provide commands
            services.AddTransient<ICreateCommand, CreateCommand>();
            services.AddTransient<IReadCommand, ReadCommand>();
            services.AddTransient<IExitCommand, ExitCommand>();
            services.AddTransient<IHelpCommand, HelpCommand>();
            services.AddTransient<IPrintHandler, PrintHandler>();
            services.AddTransient<IClearCommand, ClearCommand>();
            services.AddTransient<IDeleteCommand, DeleteCommand>();
            // provide handler
            services.AddTransient<ICommandHandler, CommandHandler>();
            services.AddTransient<IPlanConnectionHandler, PlanConnectionHandler>();
            services.AddScoped<ConsoleApplication>();
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
