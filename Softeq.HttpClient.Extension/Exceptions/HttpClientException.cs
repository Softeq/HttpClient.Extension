// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Net;
using Softeq.HttpClient.Extension.Utility;

namespace Softeq.HttpClient.Extension.Exceptions
{
    public class HttpClientException : Exception
    {
        public List<ErrorDto> Errors { get; set; } = new List<ErrorDto>();
        public HttpStatusCode StatusCode { get; set; }

        public HttpClientException(string message) : base(message)
        {
        }

        public HttpClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HttpClientException(params ErrorDto[] errors)
        {
            InitializeErorrs(errors);
        }

        protected void InitializeErorrs(IEnumerable<ErrorDto> errors)
        {
            Errors.AddRange(errors);
        }
    }
}