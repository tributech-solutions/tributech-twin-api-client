using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace Tributech.Dsk.TwinApi.Client;

public static class TwinApiClientFactory
{
    public static TwinApiClient FromHttpClient(HttpClient httpClient)
    {
        return new TwinApiClient(httpClient);
    }

    public static TwinApiClient FromOptions(TwinApiClientOptions options)
    {
        var socketsHttpHandler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        };
        var authHandler = new APIAuthHandler(options.TokenUrl, string.Join(' ', options.Scopes), options.ClientId, options.ClientSecret, socketsHttpHandler);
        var httpClient = new HttpClient(authHandler)
        {
            BaseAddress = new Uri(options.ApiEndpoint),
            Timeout = TimeSpan.FromSeconds(30)
        };

        return FromHttpClient(httpClient);
    }

    public static TwinApiClient FromConfiguration(IConfiguration config, string configSectionName = nameof(TwinApiClientOptions))
    {
        TwinApiClientOptions options = new();
        config.Bind(configSectionName, options);

        return FromOptions(options);
    }
}
