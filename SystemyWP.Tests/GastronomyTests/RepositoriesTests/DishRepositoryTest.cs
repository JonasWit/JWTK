using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Repositories;
using SystemyWP.Tests.Utilities;
using Xunit;

namespace SystemyWP.Tests.GastronomyTests.RepositoriesTests;

public class DishRepositoryTest
{
    [Fact]
    public async Task CreateDishTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("CreateDishTest"));
        IIngredientRepository repo = new IngredientRepository(context);


        //Act


        //Assert

    }
}