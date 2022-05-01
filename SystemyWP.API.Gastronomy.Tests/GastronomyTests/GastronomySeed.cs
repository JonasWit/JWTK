using System;
using System.Collections.Generic;
using SystemyWP.API.Gastronomy.Data.DTOs;
using SystemyWP.API.Gastronomy.Data.Models;

namespace SystemyWP.API.Gastronomy.Tests.GastronomyTests;

public class GastronomySeed
{
    public const string AccessKey = "seed-test-key";
        
    public static List<Ingredient> GetTestIngredientsList(int size)
    {
        var result = new List<Ingredient>();
        var rnd = new Random();

        for (var i = 0; i < size; i++)
        {
            result.Add(new Ingredient
            {
                AccessKey = AccessKey,
                Name = $"TName - {i}",
                Description = $"TDesc - {i}",
                MeasurementUnits = (MeasurementUnits)rnd.Next(0, 3),
                StackSize = rnd.NextDouble(),
                PricePerStack = rnd.Next(0, int.MaxValue)
            });
        }
        
        return result;
    }
    
    public static List<Dish> GetTestDishesList(int size)
    {
        var result = new List<Dish>();
        var rnd = new Random();

        for (var i = 0; i < size; i++)
        {
            result.Add(new Dish
            {
                AccessKey = AccessKey,
                Name = $"TName - {i}",
                Description = $"TDesc - {i}",
            });
        }
        
        return result;
    }
    
    public static List<Menu> GetTestMenusList(int size)
    {
        var result = new List<Menu>();
        var rnd = new Random();

        for (var i = 0; i < size; i++)
        {
            result.Add(new Menu
            {
                AccessKey = AccessKey,
                Name = $"TName - {i}",
                Description = $"TDesc - {i}",
            });
        }
        
        return result;
    }
}