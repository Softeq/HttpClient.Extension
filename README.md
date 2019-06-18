![Azure DevOps builds](https://dev.azure.com/eugenypetlakh/HttpClient.Extension/_apis/build/status/Softeq.HttpClient.Extension?branchName=master)
![NuGet](https://img.shields.io/nuget/v/Softeq.HttpClient.Extension.svg)

## HttpClient.Extension
This is package to extend standard **HttpClient** for send/receive messages.

## Dependencies
- Microsoft.AspNetCore
- Microsoft.Extensions.Http.Polly

## Configuration

Configure a client with **Polly** policies, in `Startup`. Insert this two private methods:

```csharp
private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}

private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
}
```

In public method `ConfigureServices` in `Startup.cs` file where **HTTPClient** is implementing add:
```csharp
services.AddHttpClient("ProfileHttpClient")
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy());
```
## Usage
Declare **HTTPClient**

```csharp
_httpClient = new RestHttpClient(new TestHttpClientFactory());
```
Send and get response
```csharp
_httpClient.SendAndGetResponseAsync(request)
```
Send and deserialize
```csharp
_httpClient.SendAndDeserializeAsync<UsersList>(request)
```

## About

This project is maintained by [Softeq Development Corp.](https://www.softeq.com/)
We specialize in .NET core applications.

 - [Facebook](https://web.facebook.com/Softeq.by/)
 - [Instagram](https://www.instagram.com/softeq/)
 - [Twitter](https://twitter.com/Softeq)
 - [Vk](https://vk.com/club21079655)

## Contributing

We welcome any contributions.

## License

The Serilog extention project is available for free use, as described by the [LICENSE](/LICENSE) (MIT).