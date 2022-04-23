using SystemyWP.API.Data.DTOs.General;

namespace SystemyWP.API.Constants;

public class ServicesConstants
{
    public struct Gastronomy
    {
        private const string BaseController = "ingredient";
        public const string HealthCheck = "health";
        public const string CreateIngredient = "ingredient";
        public const string UpdateIngredient = "ingredient";
        public static string DeleteIngredient(ResourceAccessPass pass) => $"{BaseController}/{pass.AccessKey}/{pass.Id}";
        public static string GetIngredient(ResourceAccessPass pass) => $"{BaseController}/{pass.AccessKey}/{pass.Id}";
        public static string GetIngredients(string accessKey) => $"{BaseController}/list/{accessKey}";
        public static string CountIngredients(string accessKey) => $"{BaseController}/count/{accessKey}";
        public static string GetPaginatedIngredients(string accessKey, int cursor, int take) => $"{BaseController}/list/{accessKey}/{cursor}/{take}";
    }
    
    public struct GastronomyResponseErrors
    {
        public const string InternalErrorFromService = "GASTRONOMY SERVICE - INTERNAL ERROR FROM SERVICE";
        public const string InternalUnsupportedStatusCode = "GASTRONOMY SERVICE - UNSUPPORTED STATUS CODE";
        
        public const string CreateIngredient = "GASTRONOMY SERVICE - CREATE Ingredient Failed";
        public const string GetIngredient = "GASTRONOMY SERVICE - GET Ingredient Failed";
        public const string CountIngredients = "GASTRONOMY SERVICE - GET COUNT Ingredient Failed";
        public const string GetIngredients = "GASTRONOMY SERVICE - GET Ingredients Failed";
        public const string RemoveIngredient = "GASTRONOMY SERVICE - DELETE Ingredient Failed";
        public const string UpdateIngredient = "GASTRONOMY SERVICE - UPDATE Ingredient Failed";
        
        
        
        
        
        
    }  
    
    
}