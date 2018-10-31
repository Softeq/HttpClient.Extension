// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.HttpClient.Extension.Models;

namespace Softeq.HttpClient.Extension
{
    public interface IRestHttpClient
    {
        Task<string> SendAndGetResponseAsync(BaseHttpRequest request);
        Task<T> SendAndDeserializeAsync<T>(BaseHttpRequest request);
        Task<bool> SendAsync(BaseHttpRequest request);
    }
}