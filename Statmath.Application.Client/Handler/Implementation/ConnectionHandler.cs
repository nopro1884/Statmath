using Newtonsoft.Json;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        public Task CreatePlan(PlanViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task CreatePlans(IEnumerable<PlanViewModel> viewModels)
        {
            var jsonPayload = JsonConvert.SerializeObject(viewModels.ToList());


            var uri = new UriBuilder
            {
                Host = _appSettings.Host,
                Scheme = _appSettings.Scheme,
                Path = _appSettings.Path,
                Port = _appSettings.Port,
            }.Uri;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{uri}/create_many");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = jsonPayload.Length;

            using (var webStream = request.GetRequestStream())
            using (var requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(jsonPayload);
            }
            try
            {
                WebResponse webResponse = await request.GetResponseAsync();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    Console.Out.WriteLine(response);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
        }
    }
}
