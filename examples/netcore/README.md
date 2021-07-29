## Usage of .NET Core client

Requires net5.0 or higher.

``` csharp
var authHandler = new APIAuthHandler(tokenUrl, scope, clientId, clientSecret);
var authorizedHttpClient = new HttpClient(authHandler);
authorizedHttpClient.BaseAddress = new Uri(baseUrl);
var apiClient = new TwinApiClient(authorizedHttpClient);
```

For details, see [Program.cs](./Program.cs).