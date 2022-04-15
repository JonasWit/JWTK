namespace SystemyWP.API.Gastronomy
{
    public class AppConstants
    {
        public struct DataLimits
        {
            public const int DescriptionLimit = 1000;
            public const int NameLimit = 500;
            public const int ShortKeyLimit = 128;
            public const int KeyLimit = 256;
            public const int LongKeyLimit = 512;
        }

        public struct ResponseMessages
        {
            public const string CreateIngredientException = "CREATE Ingredient Failed";
            public const string UpdateIngredientException = "UPDATE Ingredient Failed";
            public const string RemoveIngredientException = "REMOVE Ingredient Failed";
            public const string GetIngredientException = "GET Ingredient Failed";
            
            public const string CreateDishException = "CREATE Dish Failed";
            public const string UpdateDishException = "UPDATE Dish Failed";
            public const string RemoveDishException = "REMOVE Dish Failed";
            public const string GetDishException = "GET Dish Failed";
        }
    }
}