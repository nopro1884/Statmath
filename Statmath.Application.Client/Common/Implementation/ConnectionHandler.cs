using Statmath.Application.Client.Common.Abstraction;
using System;

namespace Statmath.Application.Client.Common.Implementation
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
