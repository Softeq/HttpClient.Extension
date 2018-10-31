// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Net.Http;
using System.Threading.Tasks;

namespace Softeq.HttpClient.Extension.Utility
{
    public interface IExceptionHandler
    {
        Task<HttpResponseMessage> Handle(HttpResponseMessage response);
    }
}