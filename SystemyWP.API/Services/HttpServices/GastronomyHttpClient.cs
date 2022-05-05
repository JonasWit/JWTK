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

    private string ErrorMessage(string methodName) => $"Gastronomy Service Client {methodName} Error";

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

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(CreateIngredient)));
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
        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetIngredient)));
        return await response.Content.ReadFromJsonAsync<IngredientDto>();
    }

    public async Task<List<IngredientDto>> GetIngredients(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetIngredients(accessKey)));

        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetIngredients)));
        return await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
    }

    public async Task<ElementCountDto> CountIngredients(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountIngredients(accessKey)));

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(CountIngredients)));
        return await response.Content.ReadFromJsonAsync<ElementCountDto>();
    }

    public async Task<List<IngredientDto>> GetPaginatedIngredients(string accessKey, int cursor, int take)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetPaginatedIngredients(accessKey, cursor, take)));

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetPaginatedIngredients)));
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

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(CreateDish)));
        return await response.Content.ReadFromJsonAsync<DishDto>();
    }

    public async Task<DishServiceDto> GetDish(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetDish(resourceAccessPass)));

        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetDish)));
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
        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetDishes)));
        return await response.Content.ReadFromJsonAsync<List<DishServiceDto>>();
    }

    public async Task<ElementCountDto> CountDishes(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountDishes(accessKey)));

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(CountDishes)));
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

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetPaginatedDishes)));
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
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.RemoveIngredientFromDish,
                dishIngredientUpdateDto));
        return response.StatusCode;
    }

    #endregion

    #region Menus

    public async Task<MenuDto> CreateMenu(MenuCreateDto menuCreateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.BaseMenuController, menuCreateDto));

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(CreateMenu)));
        return await response.Content.ReadFromJsonAsync<MenuDto>();
    }

    public async Task<MenuServiceDto> GetMenu(ResourceAccessPass resourceAccessPass)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetMenu(resourceAccessPass)));

        if (response.StatusCode == HttpStatusCode.BadRequest) return null;
        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetMenu)));
        return await response.Content.ReadFromJsonAsync<MenuServiceDto>();
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
        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetMenus)));
        return await response.Content.ReadFromJsonAsync<List<MenuServiceDto>>();
    }
    
    public async Task<ElementCountDto> CountMenus(string accessKey)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountMenus(accessKey)));

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(CountMenus)));
        return await response.Content.ReadFromJsonAsync<ElementCountDto>();
    }
    
    public async Task<HttpStatusCode> UpdateMenu(MenuUpdateDto menuDishUpdateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PutAsJsonAsync(UrlService.GastronomyService.BaseMenuController, menuDishUpdateDto));
        return response.StatusCode;
    }
    
    public async Task<List<MenuServiceDto>> GetPaginatedMenus(string accessKey, int cursor, int take)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetPaginatedMenus(accessKey, cursor, take)));

        if (!response.IsSuccessStatusCode) throw new Exception(ErrorMessage(nameof(GetPaginatedMenus)));
        return await response.Content.ReadFromJsonAsync<List<MenuServiceDto>>();
    }
    
    public async Task<HttpStatusCode> AddDishToMenu(MenuDishUpdateDto menuDishUpdateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.AddDishToMenu, menuDishUpdateDto));
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> RemoveDishFromMenu(MenuDishUpdateDto menuDishUpdateDto)
    {
        var response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.RemoveDishFromMenu,
                menuDishUpdateDto));
        return response.StatusCode;
    }

    #endregion
}