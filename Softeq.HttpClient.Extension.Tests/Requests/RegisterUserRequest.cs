// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Softeq.HttpClient.Extension.Models;
using Softeq.HttpClient.Extension.Tests.Models;

namespace Softeq.HttpClient.Extension.Tests.Requests
{
    class RegisterUserRequest : BaseHttpRequest
    {
        public RegisterUserRequest()
        {
        }

        public RegisterUserRequest(string endpointUrl)
        {
            EndpointUrl = endpointUrl;
        }

        public override string EndpointUrl { get; } = "https://reqres.in/api/register";

        public override HttpMethod Method => HttpMethod.Post;

        public override HttpContent GetContent()
        {
            var user = new Register
            {
                Email = "sydney@fife",
                Password = "pistol"
            };

            var content = JsonConvert.SerializeObject(user, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return new StringContent(content, Encoding.UTF8, HttpConsts.ApplicationJsonHeaderValue);
        }
    }
}