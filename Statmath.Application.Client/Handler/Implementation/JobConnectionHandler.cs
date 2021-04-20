using Microsoft.Extensions.Options;
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
    public class JobConnectionHandler : IJobConnectionHandler
    {
        private readonly Uri _uri;

        public JobConnectionHandler(IOptionsMonitor<AppSettings> optionsDelegate)
        {
            // build default uri to cosume restful api 
            _uri = new UriBuilder
            {
                Host = optionsDelegate.CurrentValue.Host,
                Scheme = optionsDelegate.CurrentValue.Scheme,
                Path = optionsDelegate.CurrentValue.Path,
                Port = optionsDelegate.CurrentValue.Port,
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

        private async Task<int> MakeDeleteRequest(string action, dynamic payload = null)
        {
            HttpWebRequest request;

            try
            {
                request = (HttpWebRequest)WebRequest.Create($"{_uri}/{action}");
                request.Method = "DELETE";
                if (payload != null)
                {
                    var json = JsonConvert.SerializeObject(payload);
                    request.ContentType = "application/json";
                    request.ContentLength = json.Length;
                    request.AutomaticDecompression = DecompressionMethods.GZip;
                    using var webStream = request.GetRequestStream();
                    using var requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII);
                    requestWriter.Write(json);
                }
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
                if (int.TryParse(response, out var affectedRows))
                {
                    return affectedRows;
                }
                throw new Exception("Unexpected response from Server");
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

        /// <summary>
        /// delete all entries from database
        /// </summary>
        /// <returns>affected rows</returns>
        public async Task<int> DeleteAll()
        {
            var response = await MakeDeleteRequest(Constants.ApiActionDeleteMany);
            return response;
        }

        /// <summary>
        /// delete a single row from database
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>affected rows</returns>
        public async Task<int> Delete(JobViewModel viewModel)
        {
            var response = await MakeDeleteRequest(Constants.ApiActionDelete, viewModel);
            return response;
        }

        /// <summary>
        /// create a viewmodel row in database
        /// </summary>
        /// <param name="viewModel">viewmodel to create</param>
        /// <returns>affected rows</returns>
        public async Task<string> CreateJob(JobViewModel viewModel)
        {
            var response = await MakePostRequest(Constants.ApiActionCreate, viewModel);
            return response;
        }

        /// <summary>
        /// create a bunch of viewmodel rows in database
        /// </summary>
        /// <param name="viewModels">viewmodels to create</param>
        /// <returns>affected rows</returns>
        public async Task<string> CreateJobs(IEnumerable<JobViewModel> viewModels)
        {
            var response = await MakePostRequest(Constants.ApiActionCreateMany, viewModels);
            return response;
        }

        /// <summary>
        /// get all jobs from database
        /// </summary>
        /// <returns>list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetAll()
        {
            var response = await MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetAll);
            return response;
        }

        /// <summary>
        /// get a single row from db by job id
        /// </summary>
        /// <param name="job">job id</param>
        /// <returns>jobs viewmodel</returns>
        public async Task<JobViewModel> GetByJob(int job)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamJob, job)
            };
            var response = await MakeGetRequest<JobViewModel>(Constants.ApiActionGetByJob, queryParameters);
            return response;
        }


        /// <summary>
        /// get jobs by machine
        /// </summary>
        /// <param name="machine">name of machine</param>
        /// <returns>a list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetByMachine(string machine)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamMachine, machine)
            };
            var response = await MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetByMachine, queryParameters);
            return response;
        }


        /// <summary>
        /// get jobs by date
        /// </summary>
        /// <param name="type">start or end</param>
        /// <param name="date">date</param>
        /// <returns>a list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetByDate(string type, string date)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamTime, type),
                new KeyValuePair<string, dynamic>(Constants.ApiParamDate, date)
            };
            var response = await MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetByDate, queryParameters);
            return response;
        }

        /// <summary>
        /// get jobs by date
        /// </summary>
        /// <param name="type">start or end</param>
        /// <param name="date">date</param>
        /// <returns>a list of jobs</returns>
        public async Task<ICollection<JobViewModel>> GetByDateTime(string type, string datetime)
        {
            var queryParameters = new List<KeyValuePair<string, dynamic>> {
                new KeyValuePair<string, dynamic>(Constants.ApiParamTime, type),
                new KeyValuePair<string, dynamic>(Constants.ApiParamDate, datetime)
            };
            var response = await MakeGetRequest<ICollection<JobViewModel>>(Constants.ApiActionGetByDateTime, queryParameters);
            return response;
        }
    }
}
