// Developed by Softeq Development Corporation
// http://www.softeq.com

using Newtonsoft.Json;

namespace Softeq.HttpClient.Extension.Tests.Models
{
    public class UsersList
    {
        public int Page { get; set; }
        [JsonProperty("per_page")]
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public User[] Data { get; set; }
    }
}