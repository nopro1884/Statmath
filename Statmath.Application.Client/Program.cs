using System.Threading.Tasks;

namespace Statmath.Application.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var appSetup = new ApplicationSetup();
            var app = appSetup.GetAppContext();
            await app.RunAsync();
        }
    }
}