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

        /// <summary>
        /// load application settings file an make it accessable for other user
        /// </summary>
        /// <returns>Configration</returns>
        private  IConfiguration GetConfiguration()
        {
            // create builderand return configuration reference
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        /// <summary>
        /// Configure and create service provider for di/ioc
        /// </summary>
        /// <returns></returns>
        private  IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            // provide app settings
            //services.AddSingleton(typeof(AppSettings), _appSettings);
            services.Configure<AppSettings>(_configuration.GetSection(nameof(AppSettings)));
            // common app stuff
            services.AddSingleton<IJobConverter, JobConverter>();
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
            services.AddTransient<IJobConnectionHandler, JobConnectionHandler>();
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
