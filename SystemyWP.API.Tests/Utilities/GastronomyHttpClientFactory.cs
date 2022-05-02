using System.Net.Http;

namespace SystemyWP.API.Tests.Utilities;

public class GastronomyHttpClientFactory: IHttpClientFactory
{
    public HttpClient CreateClient(string name) => new DummyGastronomyApplication().CreateClient();
}