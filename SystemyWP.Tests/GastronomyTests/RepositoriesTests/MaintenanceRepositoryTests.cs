using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Repositories;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;
using SystemyWP.Tests.Utilities;
using Xunit;

namespace SystemyWP.Tests.GastronomyTests.RepositoriesTests;

public class MaintenanceRepositoryTests
{
    [Fact]
    public async Task RemoveAllDataForKeyTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("RemoveAllDataForKeyTest"));

        var repo = new MaintenanceRepository(context);
        
        
        
        
        
        
        
        
        //Act
        await repo.SaveChanges();
        var results = await repo.GetIngredients(GastronomySeed.AccessKey);

        //Assert
        Assert.Equal(ingredients.Count, results.Count);
    }
}