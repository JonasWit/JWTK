using System;
using Microsoft.Extensions.Options;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Services.HttpServices;

public class UrlService
{
    public const string HealthCheck = "health";
    private readonly IOptionsMonitor<ClusterServices> _clusterServicesSettings;

    public UrlService()
    {
    }

    public UrlService(IOptionsMonitor<ClusterServices> clusterServicesSettings)
    {
        _clusterServicesSettings = clusterServicesSettings;
    }

    public Uri IngredientPath(string controllerName, long id)
    {
        return new($"{_clusterServicesSettings.CurrentValue.MasterService}/{controllerName}/{id}");
    }

    public Uri DishPath(string controllerName, long id)
    {
        return new($"{_clusterServicesSettings.CurrentValue.MasterService}/{controllerName}/{id}");
    }

    public Uri MenuPath(string controllerName, long id)
    {
        return new($"{_clusterServicesSettings.CurrentValue.MasterService}/{controllerName}/{id}");
    }

    public struct GastronomyService
    {
        public const string BaseIngredientController = "ingredient";

        public static string GetIngredient(ResourceAccessPass pass)
        {
            return $"{BaseIngredientController}/{pass.AccessKey}/{pass.Id}";
        }

        public static string DeleteIngredient(ResourceAccessPass pass)
        {
            return $"{BaseIngredientController}/{pass.AccessKey}/{pass.Id}";
        }

        public static string GetIngredients(string accessKey)
        {
            return $"{BaseIngredientController}/list/{accessKey}";
        }

        public static string CountIngredients(string accessKey)
        {
            return $"{BaseIngredientController}/count/{accessKey}";
        }

        public static string GetPaginatedIngredients(string accessKey, int cursor, int take)
        {
            return $"{BaseIngredientController}/list/{accessKey}/{cursor}/{take}";
        }

        public const string BaseDishController = "dish";
        public static string AddIngredientToDish = $"{BaseDishController}/add-ingredient";
        public static string RemoveIngredientFromDish = $"{BaseDishController}/remove-ingredient";

        public static string GetDish(ResourceAccessPass pass)
        {
            return $"{BaseDishController}/{pass.AccessKey}/{pass.Id}";
        }

        public static string DeleteDish(ResourceAccessPass pass)
        {
            return $"{BaseDishController}/{pass.AccessKey}/{pass.Id}";
        }

        public static string GetDishes(string accessKey)
        {
            return $"{BaseDishController}/list/{accessKey}";
        }

        public static string CountDishes(string accessKey)
        {
            return $"{BaseDishController}/count/{accessKey}";
        }

        public static string GetPaginatedDishes(string accessKey, int cursor, int take)
        {
            return $"{BaseDishController}/list/{accessKey}/{cursor}/{take}";
        }

        public const string BaseMenuController = "menu";
        public static string AddDishToMenu = $"{BaseMenuController}/add-dish";
        public static string RemoveDishFromMenu = $"{BaseMenuController}/remove-dish";

        public static string GetMenu(ResourceAccessPass pass)
        {
            return $"{BaseMenuController}/{pass.AccessKey}/{pass.Id}";
        }

        public static string DeleteMenu(ResourceAccessPass pass)
        {
            return $"{BaseMenuController}/{pass.AccessKey}/{pass.Id}";
        }

        public static string GetMenus(string accessKey)
        {
            return $"{BaseMenuController}/list/{accessKey}";
        }

        public static string CountMenus(string accessKey)
        {
            return $"{BaseMenuController}/count/{accessKey}";
        }

        public static string GetPaginatedMenus(string accessKey, int cursor, int take)
        {
            return $"{BaseMenuController}/list/{accessKey}/{cursor}/{take}";
        }
    }
}