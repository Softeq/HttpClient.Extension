// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Net.Http;
using Softeq.HttpClient.Extension.Utility;

namespace Softeq.HttpClient.Extension
{
    public class RestHttpClient : RestHttpClientBase
    {
        public RestHttpClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory, String.Empty, new ExceptionHandler())
        {
        }

        public RestHttpClient(IHttpClientFactory httpClientFactory, string apiUrl, string cookie)
            : base(httpClientFactory, String.Empty, new ExceptionHandler())
        {
            ApiUrl = apiUrl;
            Cookie = cookie;
            _httpClient.BaseAddress = new Uri(apiUrl);
        }

        protected override string ApiUrl { get; }
        protected override string Cookie { get; }
        protected override string AccessToken { get; set; }

        public void ChangeAccessToken(string token)
        {
            this.AccessToken = token;
        }
    }
}