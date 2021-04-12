using Microsoft.Extensions.Configuration;
using Statmath.Application.Task.Client.Common;
using System;
using System.IO;

namespace Statmath.Application.Task.Client
{
    class Program
    {
        static AppSettings appSettings = new AppSettings();
        static IConfiguration configuration;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            Console.WriteLine(appSettings.Key1);
            Console.WriteLine(appSettings.Key2);

            //var filePath = Console.ReadLine();
            //var converter = new PlanConverter();

            //var lines = File.ReadAllLines(@"C:\Users\Jan\Desktop\plan.csv", Encoding.UTF8);
            //var vms = from line in lines.Skip(1)
            //          let fields = line.Split(';')
            //          select converter.ConvertFromCsv(fields);


            //var count = vms.Count();


            Console.ReadLine();

        }
    }
}
