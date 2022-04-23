using SystemyWP.API.DTOs.General.UserForms;
using SystemyWP.API.FluentValidations;
using SystemyWP.API.FluentValidations.MasterService;
using SystemyWP.API.FluentValidations.MasterService.UserForms;
using Xunit;

namespace SystemyWP.Tests.MasterTests.ValidatorsTests;

public class AuthValidatorsTests
{
    [Fact]
    public void RegisterValidatorIncorrectTest()
    {
        //Arrange
        var validator = new UserCredentialsFormValidation();

        //Act
        var testModel = new UserCredentialsForm
        {
            Email = "ds#dsds",
            Password = ""
        };
        var res = validator.Validate(testModel);
        var errors = res.Errors;

        //Assert
        Assert.Contains(errors, item => item.ErrorMessage.Equals("'Email' is not a valid email address."));
        Assert.Contains(errors, item => item.ErrorMessage.Equals("'Password' must not be empty."));
    }
    
    [Fact]
    public void RegisterValidatorCorrectTest()
    {
        //Arrange
        var validator = new UserCredentialsFormValidation();

        //Act
        var testModel = new UserCredentialsForm
        {
            Email = "jw@jw.pl",
            Password = "12345678909876"
        };
        var res = validator.Validate(testModel);
        var errors = res.Errors;

        //Assert
        Assert.Empty(errors);
    }
}