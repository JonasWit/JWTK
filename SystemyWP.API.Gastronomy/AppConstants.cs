namespace MasterService.API.Gastronomy
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
        
        public struct RecordsLimits
        {
            public const int Ingredient = 1000;
            public const int Dish = 500;
            public const int Menu = 200;
        }
    }
}