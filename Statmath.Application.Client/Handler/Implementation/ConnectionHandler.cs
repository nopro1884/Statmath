using Statmath.Application.Client.Handler.Abstraction;
using System;

namespace Statmath.Application.Client.Handler.Implementation
{
    public class ConnectionHandler : IConnectionHandler
    {
        private readonly AppSettings _appSettings;

        public ConnectionHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
            Console.WriteLine(appSettings.Port);
        }
    }
}
