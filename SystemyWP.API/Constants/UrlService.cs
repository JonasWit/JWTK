using SystemyWP.API.DTOs.General;

namespace SystemyWP.API.Constants;

public class UrlService
{
    public struct Gastronomy
    {
        public const string CreateIngredient = "ingredient";
        public static string GetIngredient(ResourceAccessPass pass) => $"ingredient/{pass.AccessKey}/{pass.Id}";
        public static string GetIngredients(string accessKey) => $"ingredient/list/{accessKey}";
    }
    
    public struct Errors
    {
        public const string CreateIngredient = "CREATE Ingredient Failed";
        public const string GetIngredient = "GET Ingredient Failed";
    }  
    
    
}