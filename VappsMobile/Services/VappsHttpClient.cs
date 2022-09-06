using VappsMobile.Policies;

namespace VappsMobile.Services
{
    internal class VappsHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientPolicy _httpClientPolicy;

        public VappsHttpClient(IHttpClientFactory httpClientFactory,
            HttpClientPolicy httpClientPolicy)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;


        }
    }
}
