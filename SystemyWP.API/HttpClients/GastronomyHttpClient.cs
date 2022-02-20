using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SystemyWP.API.DTOs;
using SystemyWP.API.Policies;
using SystemyWP.API.Settings;
using SystemyWP.Lib.Shared.DTOs;
using SystemyWP.Lib.Shared.DTOs.Gastronomy;

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