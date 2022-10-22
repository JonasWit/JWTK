using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using MasterService.API.Policies;
using MasterService.API.Settings;

namespace MasterService.API.Services.HttpServices;

public class LogisticsHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly HttpClientPolicy _httpClientPolicy;

    private string ErrorMessage(string methodName) => $"Logistics Service Client {methodName} Error";

    public LogisticsHttpClient(
        IOptionsMonitor<ClusterServices> clusterServicesSettings,
        HttpClient httpClient,
        HttpClientPolicy httpClientPolicy)
    {
        _httpClient = httpClient;
        _httpClientPolicy = httpClientPolicy;
        _httpClient.BaseAddress = new Uri(clusterServicesSettings.CurrentValue.LogisticsService);
    }
}