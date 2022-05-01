using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SystemyWP.API.Constants;
using SystemyWP.API.Data.DTOs.Gastronomy;
using SystemyWP.API.Data.DTOs.Gastronomy.Dishes;
using SystemyWP.API.Data.DTOs.Gastronomy.Ingredients;
using SystemyWP.API.Data.DTOs.Gastronomy.Menus;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Policies;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Services.HttpServices;

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

    #region Maintenance
    
    public async Task<string> GetHealthCheckResponse()
    {
        try
        {
            var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
                => _httpClient.GetAsync(UrlService.HealthCheck));

            return response.IsSuccessStatusCode
                ? AppConstants.ServiceResponses.AliveResponse
                : AppConstants.ServiceResponses.DeadResponse;
        }
        catch (Exception)
        {
            return AppConstants.ServiceResponses.ErrorResponse;
        }
    }
    
    #endregion
    
    #region Ingredients

    public async Task<IngredientDto> CreateIngredient(IngredientCreateDto ingredientCreateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.BaseIngredientController, ingredientCreateDto));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service CREATE Ingredient Error");
        return await response.Content.ReadFromJsonAsync<IngredientDto>();
    }
    
    public async Task<HttpStatusCode> UpdateIngredient(IngredientUpdateDto ingredientUpdateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PutAsJsonAsync(UrlService.GastronomyService.BaseIngredientController, ingredientUpdateDto));
        return response.StatusCode;
    }
    
    public async Task<IngredientDto> GetIngredient(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetIngredient(resourceAccessPass)));

        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Ingredient Error");
        return await response.Content.ReadFromJsonAsync<IngredientDto>();
    }
    
    public async Task<List<IngredientDto>> GetIngredients(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetIngredients(accessKey)));
    
        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Ingredients Error");
        return await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
    }
    
    public async Task<ElementCountDto> CountIngredients(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountIngredients(accessKey)));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service COUNT Ingredients Error");
        return await response.Content.ReadFromJsonAsync<ElementCountDto>();
    }
    
    public async Task<List<IngredientDto>> GetPaginatedIngredients(string accessKey, int cursor, int take)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetPaginatedIngredients(accessKey, cursor, take)));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Paginated Ingredients Error");
        return await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
    }
    
    public async Task<HttpStatusCode> RemoveIngredient(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.DeleteAsync(UrlService.GastronomyService.DeleteIngredient(resourceAccessPass)));
        return response.StatusCode;
    }
    
    #endregion
    
    #region Dishes

    public async Task<DishDto> CreateDish(DishCreateDto dishCreateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.BaseDishController, dishCreateDto));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service CREATE Dish Error");
        return await response.Content.ReadFromJsonAsync<DishDto>();
    }
    
    public async Task<DishServiceDto> GetDish(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetDish(resourceAccessPass)));
    
        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Dish Error");
        return await response.Content.ReadFromJsonAsync<DishServiceDto>();
    }
    
    public async Task<HttpStatusCode> RemoveDish(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.DeleteAsync(UrlService.GastronomyService.DeleteDish(resourceAccessPass)));
        return response.StatusCode;
    }
    
    public async Task<List<DishServiceDto>> GetDishes(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetDishes(accessKey)));
    
        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Dishes Error");
        return await response.Content.ReadFromJsonAsync<List<DishServiceDto>>();
    }
    
    public async Task<ElementCountDto> CountDishes(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountDishes(accessKey)));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service COUNT Dishes Error");
        return await response.Content.ReadFromJsonAsync<ElementCountDto>();
    }  
    
    public async Task<HttpStatusCode> UpdateDish(DishUpdateDto dishUpdateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PutAsJsonAsync(UrlService.GastronomyService.BaseDishController, dishUpdateDto));
        return response.StatusCode;
    }
    
    public async Task<List<DishServiceDto>> GetPaginatedDishes(string accessKey, int cursor, int take)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetPaginatedDishes(accessKey, cursor, take)));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Paginated Dishes Error");
        return await response.Content.ReadFromJsonAsync<List<DishServiceDto>>();
    }
    
    public async Task<HttpStatusCode> AddIngredientToDish(DishIngredientUpdateDto dishIngredientUpdateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.AddIngredientToDish, dishIngredientUpdateDto));
        return response.StatusCode;
    }
    
    public async Task<HttpStatusCode> RemoveIngredientFromDish(DishIngredientUpdateDto dishIngredientUpdateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.RemoveIngredientFromDish, dishIngredientUpdateDto));
        return response.StatusCode;
    }
    
    #endregion

    #region Menus

    public async Task<MenuDto> CreateMenu(MenuCreateDto menuCreateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.BaseMenuController, menuCreateDto));
    
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service CREATE Menu Error");
        return await response.Content.ReadFromJsonAsync<MenuDto>();
    }  
    
    public async Task<DishServiceDto> GetMenu(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetMenu(resourceAccessPass)));
    
        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Dish Error");
        return await response.Content.ReadFromJsonAsync<DishServiceDto>();
    }
    
    public async Task<HttpStatusCode> RemoveMenu(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.DeleteAsync(UrlService.GastronomyService.DeleteMenu(resourceAccessPass)));
        return response.StatusCode;
    }
    
    public async Task<List<MenuServiceDto>> GetMenus(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetMenus(accessKey)));
    
        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception("Gastronomy Service GET Menus Error");
        return await response.Content.ReadFromJsonAsync<List<MenuServiceDto>>();
    }
    
    
    
    
    #endregion
    
}