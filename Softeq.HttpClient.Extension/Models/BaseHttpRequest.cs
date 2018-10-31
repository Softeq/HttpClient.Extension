// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Net.Http;

namespace Softeq.HttpClient.Extension.Models
{
    public abstract class BaseHttpRequest
    {
        public abstract string EndpointUrl { get; }
        public virtual HttpMethod Method => HttpMethod.Get;
        public virtual IDictionary<string, string> CustomHeaders => new Dictionary<string, string>();
        public virtual bool HasCustomHeaders => CustomHeaders != null && CustomHeaders.Count > 0;

        public virtual HttpContent GetContent()
        {
            return null;
        }
    }
}