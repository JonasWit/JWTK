using System.Linq;
using SystemyWP.API.Forms.User;
using SystemyWP.API.Forms.User.Validation;
using Xunit;

namespace SystemyWP.Tests.ValidatorsTests;

public class AuthValidatorsTests
{
    [Fact]
    public void RegisterValidatorTest()
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
}