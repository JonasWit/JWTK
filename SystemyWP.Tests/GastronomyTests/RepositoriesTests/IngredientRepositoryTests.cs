using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.Repositories;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;
using SystemyWP.Tests.Utilities;
using Xunit;

namespace SystemyWP.Tests.GastronomyTests.RepositoriesTests;

public class IngredientRepositoryTests
{
    [Fact]
    public async Task CreateIngredientTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("CreateIngredientTest"));

        IIngredientRepository repo = new IngredientRepository(context);
        var newIngredient = new Ingredient
        {
            AccessKey = "abc",
            Name = "test ingredient",
            MeasurementUnits = MeasurementUnits.Gram,
            StackSize = 10,
            PricePerStack = 150
        };

        //Act
        repo.CreateIngredient(newIngredient);
        await repo.SaveChanges();
        var results = await context.Ingredients.ToListAsync();

        //Assert
        Assert.Single(results);
        Assert.Contains(results, item => item.Equals(newIngredient with {Id = 1}));
    }
    
    [Fact]
    public async Task GetIngredientListTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("GetIngredientListTest"));

        IIngredientRepository repo = new IngredientRepository(context);
        var ingredients = GastronomySeed.GetTestIngredientsList();
        ingredients.ForEach(item => repo.CreateIngredient(item));
        
        //Act
        await repo.SaveChanges();
        var results = await repo.GetIngredients(GastronomySeed.AccessKey);

        //Assert
        Assert.Equal(ingredients.Count, results.Count);
    }

    [Fact]
    public async Task UpdateIngredientTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("UpdateIngredientTest"));
        const string accessKey = "abc";
        const string ingredientName = "test ingredient";
        const string ingredientDescription = "test description";

        IIngredientRepository repo = new IngredientRepository(context);
        var newIngredient = new Ingredient
        {
            AccessKey = accessKey,
            Name = ingredientName,
            Description = ingredientDescription,
            MeasurementUnits = MeasurementUnits.Gram,
            StackSize = 10,
            PricePerStack = 150
        };

        //Act
        repo.CreateIngredient(newIngredient);
        await repo.SaveChanges();

        await using var contextForUpdate =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("UpdateIngredientTest"));
        repo = new IngredientRepository(contextForUpdate);

        var entity = await repo.GetIngredient(new ResourceAccessPass {AccessKey = accessKey, Id = 1});

        entity.Description = $"updated-{ingredientDescription}";
        entity.Name = $"updated-{ingredientName}";
        entity.StackSize = 11;
        entity.PricePerStack = 110;

        repo.UpdateIngredient(entity);
        await repo.SaveChanges();

        var result = await repo.GetIngredient(new ResourceAccessPass {AccessKey = accessKey, Id = 1});

        //Assert
        Assert.Equal(entity with {MeasurementUnits = MeasurementUnits.Gram}, result);
    }
}