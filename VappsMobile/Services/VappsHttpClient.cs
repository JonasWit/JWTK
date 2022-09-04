using VappsMobile.Policies;

namespace VappsMobile.Services
{
    internal class VappsHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientPolicy _httpClientPolicy;

        public VappsHttpClient(
            HttpClient httpClient,
            HttpClientPolicy httpClientPolicy)
        {
            _httpClient = httpClient;
            _httpClientPolicy = httpClientPolicy;
        }


    }
}
