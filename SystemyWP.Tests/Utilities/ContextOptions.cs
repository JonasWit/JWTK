using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;

namespace SystemyWP.Tests.Utilities;

public class ContextOptions
{
    public static DbContextOptions<T> GetDefaultOptions<T>(string dbName) where T : DbContext =>
        new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase($"test-db-{dbName}")
            .Options;
}