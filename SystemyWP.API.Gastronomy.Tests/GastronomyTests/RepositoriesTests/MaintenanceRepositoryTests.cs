using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.Data.Repositories;
using SystemyWP.API.Gastronomy.Tests.Utilities;
using Xunit;

namespace SystemyWP.API.Gastronomy.Tests.GastronomyTests.RepositoriesTests;

public class MaintenanceRepositoryTests
{
    [Fact]
    public async Task RemoveAllDataForKeyTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("RemoveAllDataForKeyTest"));

        const string key1 = "k1";
        const string key2 = "k2";

        var maintenanceRepo = new MaintenanceRepository(context);
        var ingredientRepo = new IngredientRepository(context);
        var dishRepo = new DishRepository(context);
        var menuRepo = new MenuRepository(context);

        var ingredient = new Ingredient {AccessKey = key1, Name = "test"};
        var otherIngredient = new Ingredient {AccessKey = key2, Name = "test"};
        ingredientRepo.CreateIngredient(ingredient);
        ingredientRepo.CreateIngredient(otherIngredient);       
        await ingredientRepo.SaveChanges();
        
        var dish = new Dish {AccessKey = key1, Name = "test"};
        var otherDish = new Dish {AccessKey = key2, Name = "test"};
        dishRepo.CreateDish(dish);
        dishRepo.CreateDish(otherDish);       
        await dishRepo.SaveChanges();     
        
        var menu = new Menu {AccessKey = key1, Name = "test"};
        var otherMenu = new Menu {AccessKey = key2, Name = "test"};
        menuRepo.CreateMenu(menu);
        menuRepo.CreateMenu(otherMenu);       
        await menuRepo.SaveChanges();        
        
        dishRepo.AddIngredient(new ResourceAccessPass {AccessKey = key1, Id = dish.Id}, ingredient.Id);
        await dishRepo.SaveChanges();  
        
        menuRepo.AddDish(new ResourceAccessPass {AccessKey = key1, Id = menu.Id}, dish.Id);
        await menuRepo.SaveChanges();      
        
        //Act
        maintenanceRepo.RemoveAllData(key1);
        await maintenanceRepo.SaveChanges();
        
        var firstIngredientsResults = await ingredientRepo.GetIngredients(key1);
        var secondIngredientsResults = await ingredientRepo.GetIngredients(key2);
        
        var firstDishesResults = await dishRepo.GetDishes(key1);
        var secondDishesResults = await dishRepo.GetDishes(key2);
        
        var firstMenusResults = await menuRepo.GetMenus(key1);
        var secondMenusResults = await menuRepo.GetMenus(key2);
        
        //Assert
        Assert.Empty(firstIngredientsResults);
        Assert.Single(secondIngredientsResults);
        Assert.Contains(secondIngredientsResults, item => item.Equals(otherIngredient));
        
        Assert.Empty(firstDishesResults);
        Assert.Single(secondDishesResults);
        Assert.Contains(secondDishesResults, item => item.Equals(otherDish));
        
        Assert.Empty(firstMenusResults);
        Assert.Single(secondMenusResults);
        Assert.Contains(secondMenusResults, item => item.Equals(otherMenu));
    }
}