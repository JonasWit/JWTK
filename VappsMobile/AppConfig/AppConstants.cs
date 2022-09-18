using VappsMobile.Models.GeneralModels;
using VappsMobile.Views.GastroApp;

namespace VappsMobile.AppConfig
{
    public static class AppConstants
    {
        public struct AppColors
        {
            public const string Violet = "#3c2560";
            public const string Gold = "#ffd606";
            public const string Red = "#820828";
        }

        public struct Navigation
        {
            public const string PopCurrent = "..";
        }

        public struct StoredPreferenceName
        {
            public const string JWTToken = nameof(JWTToken);
        }

        public static List<Vapp> GetAvailableVapps() => new()
            {
                new Vapp { Name = "Gastro-Apka", Description = "opis", Image = Icons.GastroAppDefault, MasterPageRoute = nameof(GastroAppMasterPage)  }
            };
    }
}
