using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SystemyWP.API.Policies;
using SystemyWP.API.Settings;

namespace SystemyWP.API.HttpClients;

public class GastronomyHttpClient
{
    private readonly IOptionsMonitor<ClusterServices> _optionsMonitor;
    private readonly HttpClient _httpClient;
    private readonly HttpClientPolicy _httpClientPolicy;

    public GastronomyHttpClient(
        IOptionsMonitor<ClusterServices> optionsMonitor,
        HttpClient httpClient, 
        HttpClientPolicy httpClientPolicy)
    {
        _optionsMonitor = optionsMonitor;
        _httpClient = httpClient;
        _httpClientPolicy = httpClientPolicy;
        _httpClient.BaseAddress = new Uri(_optionsMonitor.CurrentValue.GastronomyService);    
    }
    
    public async Task<string> GetHealthCheckResponse()
    {
        try
        {
            _httpClient.BaseAddress = new Uri(_optionsMonitor.CurrentValue.GastronomyService);

            var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
                => _httpClient.GetAsync("health"));

            return response.IsSuccessStatusCode
                ? SystemyWpConstants.ServiceResponses.AliveResponse
                : SystemyWpConstants.ServiceResponses.DeadResponse;
        }
        catch (Exception)
        {
            return SystemyWpConstants.ServiceResponses.ErrorResponse;
        }
    }
}