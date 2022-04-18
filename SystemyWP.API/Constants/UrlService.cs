using SystemyWP.API.DTOs.General;

namespace SystemyWP.API.Constants;

public class UrlService
{
    public struct Gastronomy
    {
        public const string CreateIngredient = "ingredient";
        public static string DeleteIngredient(ResourceAccessPass pass) => $"ingredient/{pass.AccessKey}/{pass.Id}";
        public static string GetIngredient(ResourceAccessPass pass) => $"ingredient/{pass.AccessKey}/{pass.Id}";
        public static string GetIngredients(string accessKey) => $"ingredient/list/{accessKey}";
    }
    
    public struct GastronomyErrors
    {
        public const string InternalErrorFromService = "GASTRONOMY SERVICE - INTERNAL ERROR FROM SERVICE";
        public const string InternalUnsupportedStatusCode = "GASTRONOMY SERVICE - UNSUPPORTED STATUS CODE";
        
        public const string CreateIngredient = "GASTRONOMY SERVICE - CREATE Ingredient Failed";
        public const string GetIngredient = "GASTRONOMY SERVICE - GET Ingredient Failed";
        public const string GetIngredients = "GASTRONOMY SERVICE - GET Ingredients Failed";
        public const string RemoveIngredient = "GASTRONOMY SERVICE - DELETE Ingredient Failed";
    }  
    
    
}