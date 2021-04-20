using System.Threading.Tasks;

namespace Statmath.Application.Client
{
    internal class Program
    {
        // client entry method to start up application
        private static async Task Main(string[] args)
        {
            // load global application an run it asynchronously in background
            using var appSetup = new ApplicationSetup();
            var app = appSetup.GetAppContext();
            await app.RunAsync();
        }
    }
}