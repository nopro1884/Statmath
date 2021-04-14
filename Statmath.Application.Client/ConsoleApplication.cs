using Microsoft.Extensions.Configuration;
using Statmath.Application.Client.Common.Abstraction;
using System;
using System.Threading.Tasks;

namespace Statmath.Application.Client
{
    public class ConsoleApplication
    {
        private const string ConsolePrefix = "$ ";

        private readonly IConnectionHandler _connectionHandler;

        public ConsoleApplication(IConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
        }

        public async Task RunAsync()
        {
            Console.WriteLine(_connectionHandler == null ? "DI not working" : "DI working fine");
            Console.ReadLine();
        }
    }
}
