using Microsoft.EntityFrameworkCore;
using SystemyWP.API.Gastronomy.Data;
using SystemyWP.API.Gastronomy.Repositories;
using Xunit;

namespace SystemyWP.Tests.GastronomyTests.RepositoriesTests;

public class IngredientRepositoryTests
{
    [Fact]
    public void RegisterValidatorIncorrectTest()
    {
        // var options = new DbContextOptionsBuilder<MovieDbContext>()
        //     .UseInMemoryDatabase(databaseName: "MovieListDatabase")
        //     .Options;
        //
        // // Insert seed data into the database using one instance of the context
        // using (var context = new MovieDbContext(options))
        // {
        //     context.Movies.Add(new Movie {Id = 1, Title = "Movie 1", YearOfRelease = 2018, Genre = "Action"});
        //     context.Movies.Add(new Movie {Id = 2, Title = "Movie 2", YearOfRelease = 2018, Genre = "Action"});
        //     context.Movies.Add(nnew Movie {Id = 3, Title = "Movie 3", YearOfRelease = 2019, Genre = "Action"});
        //     context.SaveChanges();
        // }
        //
        // // Use a clean instance of the context to run the test
        // using (var context = new MovieDbContext(options))
        // {
        //     MovieRepository movieRepository = new MovieRepository(context);
        //     List<Movies> movies == movieRepository.GetAll()
        //
        //         
        //     Assert.Equal(3, movies.Count);
        
        
        //Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        using var context = new AppDbContext(options);
        
        IIngredientRepository repo = new IngredientRepository(context);

        //Act

        
        
        

        //Assert

    }
}