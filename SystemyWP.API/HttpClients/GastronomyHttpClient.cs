using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SystemyWP.API.Constants;
using SystemyWP.API.DTOs;
using SystemyWP.API.DTOs.Gastronomy;
using SystemyWP.API.DTOs.General;
using SystemyWP.API.Policies;
using SystemyWP.API.Settings;

namespace SystemyWP.API.HttpClients;

public class GastronomyHttpClient
{
    private readonly IOptionsMonitor<ClusterServices> _clusterServicesSettings;
    private readonly HttpClient _httpClient;
    private readonly HttpClientPolicy _httpClientPolicy;

    public GastronomyHttpClient(
        IOptionsMonitor<ClusterServices> clusterServicesSettings,
        HttpClient httpClient,
        HttpClientPolicy httpClientPolicy)
    {
        _clusterServicesSettings = clusterServicesSettings;
        _httpClient = httpClient;
        _httpClientPolicy = httpClientPolicy;
        _httpClient.BaseAddress = new Uri(_clusterServicesSettings.CurrentValue.GastronomyService);
    }

    public async Task<string> GetHealthCheckResponse()
    {
        try
        {
            var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
                => _httpClient.GetAsync("health"));

            return response.IsSuccessStatusCode
                ? AppConstants.ServiceResponses.AliveResponse
                : AppConstants.ServiceResponses.DeadResponse;
        }
        catch (Exception)
        {
            return AppConstants.ServiceResponses.ErrorResponse;
        }
    }

    public async Task<IngredientDto> CreateIngredient(CreateIngredientDto createIngredientDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync("ingredient/create-ingredient", createIngredientDto));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy - Ingredient POST Failed");
        return await response.Content.ReadFromJsonAsync<IngredientDto>();
    }
    
    public async Task<IngredientDto> GetIngredient(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync($"ingredient/{resourceAccessPass.AccessKey}/{resourceAccessPass.Id}"));
    
        return !response.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<IngredientDto>(await response.Content.ReadAsStringAsync());
    }
}