// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Net.Http;

namespace Softeq.HttpClient.Extension.Models
{
    public class BaseDeleteHttpRequest : BaseHttpRequest
    {
        public BaseDeleteHttpRequest() { }

        public override HttpMethod Method => HttpMethod.Delete;

        public override string EndpointUrl { get; }
    }
}