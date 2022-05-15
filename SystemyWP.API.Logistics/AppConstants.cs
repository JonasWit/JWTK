namespace SystemyWP.API.Logistics;

public class AppConstants
{
    public struct DataLimits
    {
        public const int DescriptionLimit = 1000;
        public const int NameLimit = 500;
        public const int ItemTypeLimit = 500;
        public const int ShortKeyLimit = 128;
        public const int KeyLimit = 256;
        public const int LongKeyLimit = 512;
    }
        
    public struct RecordsLimits
    {
        public const int Item = 10000;
    }
}