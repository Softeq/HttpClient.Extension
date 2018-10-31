// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Net.Http;

namespace Softeq.HttpClient.Extension.Tests
{
    class TestHttpClientFactory : IHttpClientFactory
    {
        public System.Net.Http.HttpClient CreateClient(string name)
        {
            return new System.Net.Http.HttpClient();
        }
    }
}