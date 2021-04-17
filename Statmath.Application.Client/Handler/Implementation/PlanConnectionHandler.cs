using Newtonsoft.Json;
using Statmath.Application.Client.Handler.Abstraction;
using Statmath.Application.Models;
using Statmath.Application.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Handler.Implementation
{
    public class PlanConnectionHandler : IPlanConnectionHandler
    {
        private readonly AppSettings _appSettings;
        private readonly Uri _uri;

        public PlanConnectionHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _uri = new UriBuilder
            {
                Host = _appSettings.Host,
                Scheme = _appSettings.Scheme,
                Path = _appSettings.Path,
                Port = _appSettings.Port,
            }.Uri;
        }

        private async Task<string> MakePostRequest(string action, dynamic payload)
        {
            string json;
            try
            {
                json = JsonConvert.SerializeObject(payload);

            }
            catch (Exception)
            {
                Console.WriteLine("Unable to convert payload into json");
                throw;
            }

            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest)WebRequest.Create($"{_uri}/{action}");
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = json.Length;
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using var webStream = request.GetRequestStream();
                using var requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII);
                requestWriter.Write(json);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to create Web Request");
                throw;
            }

            try
            {
                WebResponse webResponse = await request.GetResponseAsync();
                using Stream webStream = webResponse.GetResponseStream() ?? Stream.Null;
                using StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to get Web Resonse");
#if DEBUG
                Console.WriteLine(e.Message);
#endif
                throw;
            }
        }

        private async Task<T> MakeGetRequest<T>(string action, IEnumerable<KeyValuePair<string, dynamic>> parameters = null)
        {
            Uri uri;
            if (parameters?.Any() ?? false)
            {
                try
                {
                    var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                    foreach (var q in parameters)
                    {
                        var key = q.Key;
                        var value = q.Value is string ? q.Value : Convert.ToString(q.Value);
                        queryString.Add(key, value);
                    }
                    uri = new Uri($"{_uri}/{action}" + "?" + queryString.ToString());
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to parse parameters for query");
                    throw;
                }
            }
            else
            {
                uri = new Uri($"{_uri}/{action}");
            }

            try
            {
                using var client = new System.Net.Http.HttpClient(); 
                string response = await client.GetStringAsync(uri);
                var result = JsonConvert.DeserializeObject<T>(response);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to get Web Resonse");
#if DEBUG
                Console.WriteLine(e.Message);
#endif
                throw;
            }
        }

        public async Task CreatePlan(PlanViewModel viewModel)
        {
            var response = await MakePostRequest("create", viewModel);
            Console.WriteLine(response);
        }

        public async Task CreatePlans(IEnumerable<PlanViewModel> viewModels)
        {
            var response = await MakePostRequest("create_many", viewModels);
            Console.WriteLine(response);
        }

        public async Task<ICollection<PlanViewModel>> GetAll()
        {
            var response = await MakeGetRequest<ICollection<PlanViewModel>>(Constants.ApiActionGetAll);
            return response;
        }

        public async Task<PlanViewModel> GetByJob(int job)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamJob, job)
            };
            var response = await MakeGetRequest<PlanViewModel>(Constants.ApiActionGetByJob, queryParameters);
            return response;
        }

        public async Task<ICollection<PlanViewModel>> GetByMachine(string machine)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamMachine, machine)
            };
            var response = await MakeGetRequest<ICollection<PlanViewModel>>(Constants.ApiActionGetByMachine, queryParameters);
            return response;
        }

        public async Task<ICollection<PlanViewModel>> GetByDate(string type, string date)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamTime, type),
                new KeyValuePair<string, dynamic>(Constants.ApiParamDate, date)
            };
            var response = await MakeGetRequest<ICollection<PlanViewModel>>(Constants.ApiActionGetByDate, queryParameters);
            return response;
        }

        public async Task<ICollection<PlanViewModel>> GetByDateTime(string type, string datetime)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamTime, type),
                new KeyValuePair<string, dynamic>(Constants.ApiParamDate, datetime)
            };
            var response = await MakeGetRequest<ICollection<PlanViewModel>>(Constants.ApiActionGetByDateTime, queryParameters);
            return response;
        }
    }
}
