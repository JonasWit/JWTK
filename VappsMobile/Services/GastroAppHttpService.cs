using VappsMobile.Policies;

namespace VappsMobile.Services
{
    public class GastroAppHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClientPolicy _httpClientPolicy;
        private readonly string _url;

        public GastroAppHttpService(IHttpClientFactory httpClientFactory, HttpClientPolicy httpClientPolicy)
        {
            _httpClientFactory = httpClientFactory;
            _httpClientPolicy = httpClientPolicy;
            _url = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        }
    }
}
