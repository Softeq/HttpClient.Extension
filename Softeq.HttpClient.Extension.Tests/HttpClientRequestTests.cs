// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Net;
using System.Threading.Tasks;
using Softeq.HttpClient.Extension.Exceptions;
using Softeq.HttpClient.Extension.Tests.Models;
using Softeq.HttpClient.Extension.Tests.Requests;
using Xunit;

namespace Softeq.HttpClient.Extension.Tests
{
    public class HttpClientRequestTests
    {
        private IRestHttpClient _httpClient;

        public HttpClientRequestTests()
        {
            _httpClient = new RestHttpClient(new TestHttpClientFactory());
        }

        [Fact]
        public async Task Should_Response_Correct_Content_From_Google()
        {
            var request = new GoogleRequest();

            var result = await _httpClient.SendAndGetResponseAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task Should_Contain_HttpClientException_For_Bad_Request()
        {
            var request = new BadRequest();

            await Assert.ThrowsAsync<HttpClientException>(() => _httpClient.SendAndGetResponseAsync(request));
        }

        [Fact]
        public async Task Should_Contain_Single_User_In_Response()
        {
            var request = new SingleUserRequest();

            var result = await _httpClient.SendAndDeserializeAsync<SingleUser>(request);

            Assert.NotNull(result);
            Assert.Equal(2, result.Data.Id);
        }

        [Fact]
        public async Task Should_Contain_List_Of_Users_In_Response()
        {
            var request = new UsersListRequest();

            var result = await _httpClient.SendAndDeserializeAsync<UsersList>(request);

            Assert.NotNull(result);
            Assert.Equal(2, result.Page);
            Assert.Equal(3, result.PerPage);
            Assert.Equal(3, result.Data.Length);
        }

        [Fact]
        public async Task Should_Contain_404_Error_For_Single_User_Request()
        {
            var request = new SingleUserRequest("https://reqres.in/api/users/23");

            var ex = await Assert.ThrowsAsync<HttpClientException>(() => _httpClient.SendAndGetResponseAsync(request));

            Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        }

        [Fact]
        public async Task Should_Create_A_New_User()
        {
            var request = new CreateUserRequest();

            var result = await _httpClient.SendAndDeserializeAsync<CreateUser>(request);

            Assert.NotNull(result);
            Assert.Equal("morpheus", result.Name);
            Assert.Equal("leader", result.Job);
            Assert.Equal(DateTime.UtcNow.Date, result.CreatedAt.Date);
        }

        [Fact]
        public async Task Should_Response_Token_For_Register_Method()
        {
            var request = new RegisterUserRequest();

            var result = await _httpClient.SendAndDeserializeAsync<RegisterResponse>(request);

            Assert.NotNull(result);
            Assert.True(result.Token.Length > 0);
        }
    }
}