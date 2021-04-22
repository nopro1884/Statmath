using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Statmath.Application.Client.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Common.Implementation
{
    public class RequestHelper : IRequestHelper
    {
        private readonly Uri _uri;

        public RequestHelper(IOptionsMonitor<AppSettings> optionsDelegate)
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


        /// <summary>
        /// make a simple delete request with json as content-type
        /// </summary>
        /// <param name="action">action to consume</param>
        /// <param name="payload">stuff to transfer</param>
        /// <returns>amount of affected rows</returns>
        public async Task<int> MakeDeleteRequest(string action, dynamic payload = null)
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
        /// make a simple get request with typed response result
        /// </summary>
        /// <typeparam name="T">expected return type</typeparam>
        /// <param name="action">action to consume</param>
        /// <param name="parameters">parameter dictionary with identifier and value</param>
        /// <returns>object of expected return type</returns>
        public async Task<T> MakeGetRequest<T>(string action, IEnumerable<KeyValuePair<string, dynamic>> parameters = null)
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

        /// <summary>
        /// make a simple post request with simple json content-type
        /// </summary>
        /// <param name="action">action to consume</param>
        /// <param name="payload">stuff to transfer</param>
        /// <returns>the response from the server</returns>
        public async Task<string> MakePostRequest(string action, dynamic payload)
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
    }
}
