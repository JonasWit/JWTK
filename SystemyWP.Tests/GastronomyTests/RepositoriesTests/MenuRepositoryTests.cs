using System.Threading.Tasks;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Data.Models;
using SystemyWP.API.Gastronomy.DTOs;
using SystemyWP.API.Gastronomy.Repositories;
using SystemyWP.API.Gastronomy.Repositories.RepositoriesInterfaces;
using SystemyWP.Tests.Utilities;
using Xunit;

namespace SystemyWP.Tests.GastronomyTests.RepositoriesTests;

public class MenuRepositoryTests
{
    [Fact]
    public async Task GetMenuListTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("GetMenuListTest"));

        IMenuRepository repo = new MenuRepository(context);
        var ingredients = GastronomySeed.GetTestMenusList();
        ingredients.ForEach(item => repo.CreateMenu(item));
        
        //Act
        await repo.SaveChanges();
        var results = await repo.GetMenus(GastronomySeed.AccessKey);

        //Assert
        Assert.Equal(ingredients.Count, results.Count);
    }
    
    [Fact]
    public async Task AddDishToMenuTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("AddDishToMenuTest"));
        
        IDishRepository dishRepo = new DishRepository(context);
        var newDish = new Dish()
        {
            AccessKey = "abc",
            Name = "test ingredient",
        };
        
        dishRepo.CreateDish(newDish);
        await dishRepo.SaveChanges();

        IMenuRepository menuRepo = new MenuRepository(context);
        var newMenu = new Menu()
        {
            AccessKey = "abc",
            Name = "test description",
        };
        
        menuRepo.CreateMenu(newMenu);
        await menuRepo.SaveChanges();

        //Act
        menuRepo.AddDish(new ResourceAccessPass {AccessKey = "abc", Id = newMenu.Id}, newDish.Id);
        var result = await menuRepo.GetMenu(new ResourceAccessPass {AccessKey = "abc", Id = newMenu.Id});

        //Assert
        Assert.Single(result.Dishes);
        Assert.Contains(result.Dishes, item => item.Equals(newDish));
    }
    
    [Fact]
    public async Task RemoveDishFromMenuTest()
    {
        //Arrange
        await using var context =
            new AppDbContext(ContextOptions.GetDefaultOptions<AppDbContext>("RemoveDishFromMenuTest"));
        
        IDishRepository dishRepo = new DishRepository(context);
        var newDish = new Dish()
        {
            AccessKey = "abc",
            Name = "test ingredient",
        };
        
        dishRepo.CreateDish(newDish);
        await dishRepo.SaveChanges();

        IMenuRepository menuRepo = new MenuRepository(context);
        var newMenu = new Menu()
        {
            AccessKey = "abc",
            Name = "test description",
        };
        
        menuRepo.CreateMenu(newMenu);
        await menuRepo.SaveChanges();

        //Act
        menuRepo.AddDish(new ResourceAccessPass {AccessKey = "abc", Id = newMenu.Id}, newDish.Id);
        await menuRepo.SaveChanges();
        
        menuRepo.RemoveDish(new ResourceAccessPass {AccessKey = "abc", Id = newMenu.Id}, newDish.Id);
        var result = await menuRepo.GetMenu(new ResourceAccessPass {AccessKey = "abc", Id = newDish.Id});

        //Assert
        Assert.Empty(result.Dishes);
    }
    
    [Fact]
    public void PaginatedResultsTests()
    {
        //Arrange


        //Act

        //Assert
 
    }
}