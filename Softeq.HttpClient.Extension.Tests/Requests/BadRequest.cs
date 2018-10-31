// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.HttpClient.Extension.Models;

namespace Softeq.HttpClient.Extension.Tests.Requests
{
    class BadRequest : BaseHttpRequest
    {
        public override string EndpointUrl { get; } = "https://bad.siteurl";
    }
}