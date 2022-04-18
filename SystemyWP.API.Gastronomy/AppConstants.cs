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
            public const string MaintenanceRemoveData = "REMOVE All for access key Failed";
            
            public const string CreateIngredientException = "CREATE Ingredient Failed";
            public const string UpdateIngredientException = "UPDATE Ingredient Failed";
            public const string RemoveIngredientException = "REMOVE Ingredient Failed";
            public const string GetIngredientException = "GET Ingredient Failed";
            public const string GetIngredientsException = "GET Ingredients Failed";
            
            public const string CreateDishException = "CREATE Dish Failed";
            public const string UpdateDishException = "UPDATE Dish Failed";
            public const string RemoveDishException = "REMOVE Dish Failed";
            public const string GetDishException = "GET Dish Failed";
            public const string GetDishesException = "GET Dishes Failed";
            
            public const string CreateMenuException = "CREATE Menu Failed";
            public const string UpdateMenuException = "UPDATE Menu Failed";
            public const string RemoveMenuException = "REMOVE Menu Failed";
            public const string GetMenuException = "GET Menu Failed";
            public const string GetMenusException = "GET Menus Failed";
        }
    }
}