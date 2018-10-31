// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Softeq.HttpClient.Extension.Models
{
    public abstract class BasePostHttpRequest<T> : BaseHttpRequest
    {
        private readonly T _dto;

        protected BasePostHttpRequest(T dto)
        {
            _dto = dto;
        }

        public override HttpMethod Method => HttpMethod.Post;

        public override HttpContent GetContent()
        {
            var content = JsonConvert.SerializeObject(_dto);
            return new StringContent(content, Encoding.UTF8, HttpConsts.ApplicationJsonHeaderValue);
        }
    }
}