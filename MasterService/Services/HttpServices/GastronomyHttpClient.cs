using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MasterService.API.Constants;
using MasterService.API.Data.DTOs.Gastronomy;
using MasterService.API.Data.DTOs.Gastronomy.Dishes;
using MasterService.API.Data.DTOs.Gastronomy.Ingredients;
using MasterService.API.Data.DTOs.Gastronomy.Menus;
using MasterService.API.Data.DTOs.General;
using MasterService.API.Policies;

namespace MasterService.API.Services.HttpServices;

public class GastronomyHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly HttpClientPolicy _httpClientPolicy;

    public GastronomyHttpClient(
        HttpClient httpClient,
        HttpClientPolicy httpClientPolicy)
    {
        _httpClient = httpClient;
        _httpClientPolicy = httpClientPolicy;
    }

    private string ErrorMessage(string methodName) => $"Gastronomy Service Client {methodName} Error";

    #region Maintenance

    public async Task<string> GetHealthCheckResponse()
    {
        try
        {
            HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
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

    public async Task<HttpStatusCode> RemoveAllData(string accessKey)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
                => _httpClient.DeleteAsync(UrlService.GastronomyService.DeleteAllData(accessKey)));
        return response.StatusCode;
    }

    #endregion

    #region Ingredients

    public async Task<IngredientDto> CreateIngredient(IngredientCreateDto ingredientCreateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.BaseIngredientController, ingredientCreateDto));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(CreateIngredient)))
            : await response.Content.ReadFromJsonAsync<IngredientDto>();
    }

    public async Task<HttpStatusCode> UpdateIngredient(IngredientUpdateDto ingredientUpdateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PutAsJsonAsync(UrlService.GastronomyService.BaseIngredientController, ingredientUpdateDto));
        return response.StatusCode;
    }

    public async Task<IngredientDto> GetIngredient(ResourceAccessPass resourceAccessPass)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetIngredient(resourceAccessPass)));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetIngredient)))
            : await response.Content.ReadFromJsonAsync<IngredientDto>();
    }

    public async Task<List<IngredientDto>> GetIngredients(string accessKey)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetIngredients(accessKey)));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetIngredients)))
            : await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
    }

    public async Task<ElementCountDto> CountIngredients(string accessKey)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountIngredients(accessKey)));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(CountIngredients)))
            : await response.Content.ReadFromJsonAsync<ElementCountDto>();
    }

    public async Task<List<IngredientDto>> GetPaginatedIngredients(string accessKey, int cursor, int take)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetPaginatedIngredients(accessKey, cursor, take)));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetPaginatedIngredients)))
            : await response.Content.ReadFromJsonAsync<List<IngredientDto>>();
    }

    public async Task<HttpStatusCode> RemoveIngredient(ResourceAccessPass resourceAccessPass)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.DeleteAsync(UrlService.GastronomyService.DeleteIngredient(resourceAccessPass)));
        return response.StatusCode;
    }

    #endregion

    #region Dishes

    public async Task<DishDto> CreateDish(DishCreateDto dishCreateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.BaseDishController, dishCreateDto));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(CreateDish)))
            : await response.Content.ReadFromJsonAsync<DishDto>();
    }

    public async Task<DishServiceDto> GetDish(ResourceAccessPass resourceAccessPass)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetDish(resourceAccessPass)));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetDish)))
            : await response.Content.ReadFromJsonAsync<DishServiceDto>();
    }

    public async Task<HttpStatusCode> RemoveDish(ResourceAccessPass resourceAccessPass)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.DeleteAsync(UrlService.GastronomyService.DeleteDish(resourceAccessPass)));
        return response.StatusCode;
    }

    public async Task<List<DishServiceDto>> GetDishes(string accessKey)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetDishes(accessKey)));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetDishes)))
            : await response.Content.ReadFromJsonAsync<List<DishServiceDto>>();
    }

    public async Task<ElementCountDto> CountDishes(string accessKey)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountDishes(accessKey)));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(CountDishes)))
            : await response.Content.ReadFromJsonAsync<ElementCountDto>();
    }

    public async Task<HttpStatusCode> UpdateDish(DishUpdateDto dishUpdateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PutAsJsonAsync(UrlService.GastronomyService.BaseDishController, dishUpdateDto));
        return response.StatusCode;
    }

    public async Task<List<DishServiceDto>> GetPaginatedDishes(string accessKey, int cursor, int take)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetPaginatedDishes(accessKey, cursor, take)));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetPaginatedDishes)))
            : await response.Content.ReadFromJsonAsync<List<DishServiceDto>>();
    }

    public async Task<HttpStatusCode> AddIngredientToDish(DishIngredientUpdateDto dishIngredientUpdateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.AddIngredientToDish, dishIngredientUpdateDto));
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> RemoveIngredientFromDish(DishIngredientUpdateDto dishIngredientUpdateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.RemoveIngredientFromDish,
                dishIngredientUpdateDto));
        return response.StatusCode;
    }

    #endregion

    #region Menus

    public async Task<MenuDto> CreateMenu(MenuCreateDto menuCreateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.BaseMenuController, menuCreateDto));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(CreateMenu)))
            : await response.Content.ReadFromJsonAsync<MenuDto>();
    }

    public async Task<MenuServiceDto> GetMenu(ResourceAccessPass resourceAccessPass)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetMenu(resourceAccessPass)));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetMenu)))
            : await response.Content.ReadFromJsonAsync<MenuServiceDto>();
    }

    public async Task<HttpStatusCode> RemoveMenu(ResourceAccessPass resourceAccessPass)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.DeleteAsync(UrlService.GastronomyService.DeleteMenu(resourceAccessPass)));
        return response.StatusCode;
    }

    public async Task<List<MenuServiceDto>> GetMenus(string accessKey)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetMenus(accessKey)));

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetMenus)))
            : await response.Content.ReadFromJsonAsync<List<MenuServiceDto>>();
    }

    public async Task<ElementCountDto> CountMenus(string accessKey)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.CountMenus(accessKey)));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(CountMenus)))
            : await response.Content.ReadFromJsonAsync<ElementCountDto>();
    }

    public async Task<HttpStatusCode> UpdateMenu(MenuUpdateDto menuDishUpdateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PutAsJsonAsync(UrlService.GastronomyService.BaseMenuController, menuDishUpdateDto));
        return response.StatusCode;
    }

    public async Task<List<MenuServiceDto>> GetPaginatedMenus(string accessKey, int cursor, int take)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.GetAsync(UrlService.GastronomyService.GetPaginatedMenus(accessKey, cursor, take)));

        return !response.IsSuccessStatusCode
            ? throw new Exception(ErrorMessage(nameof(GetPaginatedMenus)))
            : await response.Content.ReadFromJsonAsync<List<MenuServiceDto>>();
    }

    public async Task<HttpStatusCode> AddDishToMenu(MenuDishUpdateDto menuDishUpdateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.AddDishToMenu, menuDishUpdateDto));
        return response.StatusCode;
    }

    public async Task<HttpStatusCode> RemoveDishFromMenu(MenuDishUpdateDto menuDishUpdateDto)
    {
        HttpResponseMessage response = await _httpClientPolicy.ExponentialHttpRetry.ExecuteAsync(()
            => _httpClient.PostAsJsonAsync(UrlService.GastronomyService.RemoveDishFromMenu,
                menuDishUpdateDto));
        return response.StatusCode;
    }

    #endregion
}