// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.HttpClient.Extension.Models;

namespace Softeq.HttpClient.Extension.Tests.Requests
{
    class UsersListRequest : BaseHttpRequest
    {
        public UsersListRequest()
        {
        }

        public UsersListRequest(string endpointUrl)
        {
            EndpointUrl = endpointUrl;
        }

        public override string EndpointUrl { get; } = "https://reqres.in/api/users?page=2";
    }
}