using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories;
using SystemyWP.API.Gastronomy.Data.Repositories.RepositoriesInterfaces;
using SystemyWP.API.Gastronomy.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Gastronomy.Tests.RepositoryTests;

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

        var repo = new IngredientRepository(context);
        var ingredients = GastronomySeed.GetTestIngredientsList(10);
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
    
    [Fact]
    public async Task PaginatedResultsTests()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("PaginatedResultsTests"));
        var repo = new IngredientRepository(context);
        var ingredients = GastronomySeed.GetTestIngredientsList(500); 
        ingredients.ForEach(item => repo.CreateIngredient(item));
        
        //Act
        await repo.SaveChanges();

        var paginatedResults = new List<List<Ingredient>>();

        var cursor = 0;
        for (var i = 0; i < 15; i++)
        {
            paginatedResults.Add(await repo.GetIngredients(GastronomySeed.AccessKey, cursor, 75));
            cursor += 75;
        }
        
        //Assert
        Assert.Equal(500, paginatedResults.Sum(l => l.Count));      
        Assert.Equal(ingredients, paginatedResults.SelectMany(l => l).ToList());
    }  
}