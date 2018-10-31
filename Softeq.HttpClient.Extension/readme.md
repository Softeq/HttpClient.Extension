## Steps to include Polly in project using Curcuit Breaker pattern:
1. Have your project grab the `ASPNET Core 2.1` packages from `nuget`. You'll typically need the `AspNetCore` metapackage, and the extension package `Microsoft.Extensions.Http.Polly`.

2. Configure a client with *Polly* policies, in `Startup`. Insert this two private methods:
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

3. In your standard `Startup.ConfigureServices(...)` method, start by configuring a named client as below:
```csharp
services.AddHttpClient("ProfileHttpClient")
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCircuitBreakerPolicy());
where ProfileHttpClient - your HTTP client implementation
```