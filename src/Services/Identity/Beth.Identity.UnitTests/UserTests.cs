using Beth.Identity.Application.Models;
using Beth.Identity.Application.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Beth.Identity.UnitTests;

public class Tests
{
    private UserModelValidator _validator;
    
    [SetUp]
    public void Setup()
    {
        _validator = new UserModelValidator();
    }

    [Test]
    public void UserModelMobilePhoneIsValid()
    {
        var userModel = new UserModel("1234567890");
        var result = _validator.TestValidate(userModel);
        result.ShouldNotHaveValidationErrorFor(x => x.MobilePhone);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("123abc7890")]
    [TestCase("123")]
    [TestCase("12345678901234567890")]
    public void UserModelMobilePhoneIsInvalid(string phoneNumber)
    {
        var userModel = new UserModel(phoneNumber);
        var result = _validator.TestValidate(userModel);
        result.ShouldHaveValidationErrorFor(x => x.MobilePhone);
    }
}