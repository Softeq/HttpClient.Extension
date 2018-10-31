// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Softeq.HttpClient.Extension.Models;
using Softeq.HttpClient.Extension.Tests.Models;

namespace Softeq.HttpClient.Extension.Tests.Requests
{
    class CreateUserRequest : BaseHttpRequest
    {
        public CreateUserRequest()
        {
        }

        public CreateUserRequest(string endpointUrl)
        {
            EndpointUrl = endpointUrl;
        }

        public override string EndpointUrl { get; } = "https://reqres.in/api/users";

        public override HttpMethod Method => HttpMethod.Post;

        public override HttpContent GetContent()
        {
            var user = new CreateUser
            {
                Name = "morpheus",
                Job = "leader"
            };

            var content = JsonConvert.SerializeObject(user);
            return new StringContent(content, Encoding.UTF8, HttpConsts.ApplicationJsonHeaderValue);
        }
    }
}