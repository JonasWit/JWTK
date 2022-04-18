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

public class DishRepositoryTest
{
    [Fact]
    public async Task CreateDishTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("CreateDishTest"));

        IDishRepository repo = new DishRepository(context);
        var newDish = new Dish
        {
            AccessKey = "abc",
            Name = "test description",
        };

        //Act
        repo.CreateDish(newDish);
        await repo.SaveChanges();
        var results = await context.Dishes.ToListAsync();

        //Assert
        Assert.Single(results);
        Assert.Contains(results, item => item.Equals(newDish with {Id = 1}));
    }
    
    [Fact]
    public async Task AddIngredientToDishTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("AddIngredientToDishTest"));
        
        IIngredientRepository ingredientRepo = new IngredientRepository(context);
        var newIngredient = new Ingredient
        {
            AccessKey = "abc",
            Name = "test ingredient",
            MeasurementUnits = MeasurementUnits.Gram,
            StackSize = 10,
            PricePerStack = 150
        };
        
        ingredientRepo.CreateIngredient(newIngredient);
        await ingredientRepo.SaveChanges();

        IDishRepository dishRepo = new DishRepository(context);
        var newDish = new Dish
        {
            AccessKey = "abc",
            Name = "test description",
        };
        
        dishRepo.CreateDish(newDish);
        await dishRepo.SaveChanges();

        //Act
        dishRepo.AddIngredient(new ResourceAccessPass {AccessKey = "abc", Id = newDish.Id}, newIngredient.Id);
        var result = await dishRepo.GetDish(new ResourceAccessPass {AccessKey = "abc", Id = newDish.Id});

        //Assert
        Assert.Single(result.Ingredients);
        Assert.Contains(result.Ingredients, item => item.Equals(newIngredient));
    }
    
    [Fact]
    public async Task RemoveIngredientFromDishTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("RemoveIngredientFromDishTest"));
        
        IIngredientRepository ingredientRepo = new IngredientRepository(context);
        var newIngredient = new Ingredient
        {
            AccessKey = "abc",
            Name = "test ingredient",
            MeasurementUnits = MeasurementUnits.Gram,
            StackSize = 10,
            PricePerStack = 150
        };
        
        ingredientRepo.CreateIngredient(newIngredient);
        await ingredientRepo.SaveChanges();

        IDishRepository dishRepo = new DishRepository(context);
        var newDish = new Dish
        {
            AccessKey = "abc",
            Name = "test description",
        };
        
        dishRepo.CreateDish(newDish);
        await dishRepo.SaveChanges();

        //Act
        dishRepo.RemoveIngredient(new ResourceAccessPass {AccessKey = "abc", Id = newDish.Id}, newIngredient.Id);
        var result = await dishRepo.GetDish(new ResourceAccessPass {AccessKey = "abc", Id = newDish.Id});

        //Assert
        Assert.Empty(result.Ingredients);
    }
}