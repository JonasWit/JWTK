using System;
using Microsoft.Extensions.Options;

namespace SystemyWP.API.Gastronomy.Services;

public class UrlService
{
    private readonly IOptionsMonitor<ClusterServices> _clusterServicesSettings;

    public UrlService(IOptionsMonitor<ClusterServices> clusterServicesSettings)
    {
        _clusterServicesSettings = clusterServicesSettings;
    }

    public Uri GetIngredientUrl(long id) => new Uri($"{_clusterServicesSettings.CurrentValue.GastronomyService}/Ingredients/{id}");
}