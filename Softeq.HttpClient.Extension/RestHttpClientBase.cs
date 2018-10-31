// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Softeq.HttpClient.Extension.Exceptions;
using Softeq.HttpClient.Extension.Models;
using Softeq.HttpClient.Extension.Utility;

namespace Softeq.HttpClient.Extension
{
    public abstract class RestHttpClientBase : IRestHttpClient, IDisposable
    {
        protected IExceptionHandler _exceptionHandler;
        protected abstract string ApiUrl { get; }
        protected virtual string AccessToken { get; set; }
        protected virtual string Cookie { get; }
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly System.Net.Http.HttpClient _httpClient;

        public RestHttpClientBase(IHttpClientFactory httpClientFactory, string clientName, IExceptionHandler exceptionHandler)
        {
            _httpClientFactory = httpClientFactory;
            _exceptionHandler = exceptionHandler;
            _httpClient = _httpClientFactory.CreateClient(clientName);
        }

        public virtual async Task<string> SendAndGetResponseAsync(BaseHttpRequest request)
        {
            var response = await SendAsyncImpl(request);
            if (response == null)
            {
                return null;
            }

            var stringResponse = await response.Content.ReadAsStringAsync();

            return stringResponse;
        }

        public virtual async Task<T> SendAndDeserializeAsync<T>(BaseHttpRequest request)
        {
            var stringResponse = await SendAndGetResponseAsync(request);
            return stringResponse == null ? default(T) : JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public virtual async Task<bool> SendAsync(BaseHttpRequest request)
        {
            var result = default(bool);

            var response = await SendAsyncImpl(request);
            if (response != null)
            {
                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        protected virtual async Task<HttpResponseMessage> SendAsyncImpl(BaseHttpRequest request)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpConsts.ApplicationJsonHeaderValue));
            _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };

            HttpResponseMessage response;

            try
            {
                var httpRequest = new HttpRequestMessage
                {
                    Method = request.Method,
                    RequestUri = GetFullUrl(request)
                };

                if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
                {
                    httpRequest.Content = request.GetContent();
                }

                if (!string.IsNullOrWhiteSpace(AccessToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(HttpConsts.Bearer, AccessToken);
                }
                else if (!string.IsNullOrWhiteSpace(Cookie))
                {
                    _httpClient.DefaultRequestHeaders.Add("Cookie", Cookie);
                }
                if (request.HasCustomHeaders)
                {
                    foreach (var header in request.CustomHeaders)
                    {
                        httpRequest.Headers.Add(header.Key, header.Value);
                    }
                }

                response = await _httpClient.SendAsync(httpRequest);
            }
            catch (Exception ex)
            {
                throw new HttpClientException(ex.Message, ex);
            }

            response = await _exceptionHandler.Handle(response);

            return response;
        }

        protected virtual Uri GetFullUrl(BaseHttpRequest request)
        {
            return new Uri($"{ApiUrl}{request.EndpointUrl}");
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}