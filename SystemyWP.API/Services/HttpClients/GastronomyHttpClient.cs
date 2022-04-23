using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SystemyWP.API.Constants;
using SystemyWP.API.DTOs.Gastronomy;
using SystemyWP.API.DTOs.General;
using SystemyWP.API.Policies;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Services.HttpClients;

public class GastronomyHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly HttpClientPolicy _httpClientPolicy;

    public GastronomyHttpClient(
        IOptionsMonitor<ClusterServices> clusterServicesSettings,
        HttpClient httpClient,
        HttpClientPolicy httpClientPolicy)
    {
        _httpClient = httpClient;
        _httpClientPolicy = httpClientPolicy;
        _httpClient.BaseAddress = new Uri(clusterServicesSettings.CurrentValue.GastronomyService);
    }

    public async Task<string> GetHealthCheckResponse()
    {
        try
        {
            var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
                => _httpClient.GetAsync(ServicesConstants.Gastronomy.HealthCheck));

            return response.IsSuccessStatusCode
                ? AppConstants.ServiceResponses.AliveResponse
                : AppConstants.ServiceResponses.DeadResponse;
        }
        catch (Exception)
        {
            return AppConstants.ServiceResponses.ErrorResponse;
        }
    }

    public async Task<IngredientDto> CreateIngredient(IngredientCreateDto ingredientCreateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(ServicesConstants.Gastronomy.CreateIngredient, ingredientCreateDto));
    
        if (!response.IsSuccessStatusCode) throw new Exception(ServicesConstants.GastronomyErrors.CreateIngredient);
        return await response.Content.ReadFromJsonAsync<IngredientDto>();
    }
    
    public async Task<HttpStatusCode> UpdateIngredient(IngredientDto ingredientDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PutAsJsonAsync(ServicesConstants.Gastronomy.UpdateIngredient, ingredientDto));
        return response.StatusCode;
    }
    
    public async Task<IngredientDto> GetIngredient(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(ServicesConstants.Gastronomy.GetIngredient(resourceAccessPass)));
    
        if (!response.IsSuccessStatusCode) throw new Exception(ServicesConstants.GastronomyErrors.GetIngredient);
        return await response.Content.ReadFromJsonAsync<IngredientDto>();
    }
    
    public async Task<List<IngredientDto>> GetIngredients(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(ServicesConstants.Gastronomy.GetIngredients(accessKey)));
    
        if (!response.IsSuccessStatusCode) throw new Exception(ServicesConstants.GastronomyErrors.GetIngredient);
        return await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
    }
    
    public async Task<List<IngredientDto>> GetPaginatedIngredients(string accessKey, int cursor, int take)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(ServicesConstants.Gastronomy.GetPaginatedIngredients(accessKey, cursor, take)));
    
        if (!response.IsSuccessStatusCode) throw new Exception(ServicesConstants.GastronomyErrors.GetIngredient);
        return await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
    }
    
    public async Task<HttpStatusCode> RemoveIngredient(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.DeleteAsync(ServicesConstants.Gastronomy.DeleteIngredient(resourceAccessPass)));
        return response.StatusCode;
    }
}