// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Softeq.HttpClient.Extension.Exceptions;

namespace Softeq.HttpClient.Extension.Utility
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async Task<HttpResponseMessage> Handle(HttpResponseMessage response)
        {
            if (response == null || response.IsSuccessStatusCode)
            {
                return response;
            }

            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpClientException(response.ReasonPhrase) { StatusCode = response.StatusCode };
            }

            if (string.IsNullOrEmpty(responseString))
            {
                throw new HttpClientException("Something went wrong during your request.") { StatusCode = response.StatusCode };
            }

            // Try parse response 
            var errorDto = JsonConvert.DeserializeObject<ErrorDto>(responseString);
            if (errorDto != null)
            {
                throw new HttpClientException(new ErrorDto(errorDto.Code, errorDto.Description));
            }

            throw new HttpClientException("Something went wrong during your request.") { StatusCode = response.StatusCode };
        }
    }
}