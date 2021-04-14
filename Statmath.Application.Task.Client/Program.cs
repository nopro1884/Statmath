using Microsoft.Extensions.Configuration;
using Statmath.Application.Task.Client.Common;
using System;
using System.IO;

namespace Statmath.Application.Task.Client
{
    internal class Program
    {
        private static AppSettings appSettings = new AppSettings();
        private static IConfiguration configuration;
        private static bool isActive = true;
        private static string consolePrefix = "$ ";

        private static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            Runner();



            //Console.Write("Performing some task... ");
            //using (var progress = new ProgressBar())
            //{
            //    for (int i = 0; i <= 100; i++)
            //    {
            //        progress.Report((double)i / 100);
            //        Thread.Sleep(20);
            //    }
            //}
            //Console.WriteLine("Done.");

            //var filePath = Console.ReadLine();
            //var converter = new PlanConverter();

            //var lines = File.ReadAllLines(@"C:\Users\Jan\Desktop\plan.csv", Encoding.UTF8);
            //var vms = from line in lines.Skip(1)
            //          let fields = line.Split(';')
            //          select converter.ConvertFromCsv(fields);

            //var count = vms.Count();

            Console.ReadLine();
        }

        //private static string GetDbConnectionString(DatabaseSettings settings)
        //{

        //}

        private static void Runner()
        {
            do
            {
                Console.Write(consolePrefix);
                var userInput = Console.ReadLine().TrimStart().TrimEnd();
                var command = userInput.Split(' ')[0];

                switch (command)
                {
                    case "":
                        Console.WriteLine("Enter command pls...");
                        break;
                    case "--help":
                        WriteHelpMenu();
                        break;
                    case "upload" when userInput.Split(' ').Length == 2:
                        break;
                    case "readall":
                        break;
                    case "read" when userInput.Split(' ').Length == 3:
                        break;
                    case "exit":
                        //Environment.Exit(0);
                        isActive = false;
                        break;
                    case "clr":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine($"\"{command}\" is a unknown command. For more information enter \"--help\"");
                        break;
                }
            } while (isActive);
        }

        private static void WriteHelpMenu()
        {
            Console.WriteLine("\"upload <path>\"        --> get data from csv and upload it to database");
            Console.WriteLine("\"readall\"              --> read all data from database and print it to console");
            Console.WriteLine("\"read <status> <day>\"  --> exp. read end 01-01-2000");
            Console.WriteLine("\"clr\"                  --> clear console window");
            Console.WriteLine("\"exit\"                 --> exit the programm");
        }

        private static string GetApiUrl()
            => new UriBuilder
            {
                Scheme = appSettings.Scheme,
                Host = appSettings.Host,
                Port = appSettings.Port,
                Path = appSettings.Path
            }.ToString();
    }
}