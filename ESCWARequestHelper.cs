// $Id$
//
// (C) Copyright 2019 Micro Focus or one of its affiliates.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MicroFocus.COBOL.Logger;

namespace MicroFocus.EnterpriseServer.ESCWA
{
    internal class ESCWAErrorDetail
    {
        public string ErrorTitle;
        public string ErrorMessage;
        public string ErrorCode;
    }

    internal class ESCWARequestHelper
    {
        private static readonly Lazy<ILogger> g_logger = new Lazy<ILogger>(() => LogFactory.GetLogger(typeof(ESCWARequestHelper)));
        private static ILogger Logger
        {
            get
            {
                return g_logger.Value;
            }
        }

        private static HttpClient Client;

        private readonly string _host;
        private readonly int _port;

        static ESCWARequestHelper()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // NoNLS
        }

        internal ESCWARequestHelper(string host, int port)
        {
            _host = host;
            _port = port;
        }

        /// <summary>
        /// Add the required headers for the request, as per the ESCWA documentation
        /// </summary>
        /// <param name="request"></param>
        internal void AddRequestHeaders(HttpRequestMessage request)
        {
            string address = string.Format("{0}:{1}", _host, _port); // NoNLS
            request.Headers.Add("X-Requested-With", "XMLHttpRequest"); // NoNLS
            request.Headers.Add("host", address); /// NoNLS
            request.Headers.Add("Origin", "http://" + address); // NonLS
        }

        /// <summary>
        /// Get the requested information from ESCWA, deserialized to an object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="requestUrl">Url to get the information</param>
        /// <returns></returns>
        public async Task<T> GetInfoAsync<T>(string requestUrl)
        {
            T info;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            AddRequestHeaders(request);

            var response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                // If NewtonSoft.Json can't be used the following could be used instead but 
                // will then need [DataContract] added to all classes and [DataMember] to all fields.
                //                var stream = await content.ReadAsStreamAsync();
                //                var serializer = new DataContractJsonSerializer(typeof(T));
                //                info = (T)serializer.ReadObject(stream);
                var json = await content.ReadAsStringAsync();
                info = JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                info = default(T);

                var errorContent = await response.Content.ReadAsStringAsync();
                ESCWAErrorDetail errorInfo = JsonConvert.DeserializeObject<ESCWAErrorDetail>(errorContent);
                Logger.LogError("HttpRequest failed: ErrorTitle={0},ErrorMessage={1},ErrorCode={2}", errorInfo.ErrorTitle, errorInfo.ErrorMessage, errorInfo.ErrorCode);
            }
            return info;
        }

        /// <summary>
        /// Pass an object to ESCWA via a POST or PUT
        /// </summary>
        /// <typeparam name="T">Type of object to send</typeparam>
        /// <param name="post">POST if true, PUT if false</param>
        /// <param name="requestUrl">ESCWA URL</param>
        /// <param name="content">Object to send</param>
        /// <returns></returns>
        public async Task<bool> SendInfoAsync<T>(bool post, string requestUrl, T content)
        {
            HttpMethod method = post ? HttpMethod.Post : HttpMethod.Put;
            var request = new HttpRequestMessage(method, requestUrl);
            AddRequestHeaders(request);
            string json = JsonConvert.SerializeObject(content);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json"); // NoNLS

            var response = await Client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteInfoAsync(string requestUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
            AddRequestHeaders(request);

            var response = await Client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
