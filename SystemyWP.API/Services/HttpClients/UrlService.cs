using Microsoft.Extensions.Options;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Services.HttpClients;

public class UrlService
{
    private readonly IOptionsMonitor<ClusterServices> _clusterServicesSettings;

    public UrlService(IOptionsMonitor<ClusterServices> clusterServicesSettings)
    {
        _clusterServicesSettings = clusterServicesSettings;
    }
    
    
}