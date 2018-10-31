// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.HttpClient.Extension.Utility
{
    public class ErrorDto
    {
        public ErrorDto(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}