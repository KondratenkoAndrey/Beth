using Beth.Identity.Domain.Authenticate;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Beth.Identity.UnitTests.Domain.Authenticate.OneTimeCodeTests;

public class CreateOneTimeCode
{
    [Test]
    public void OneTimeCodeMustBeMoreThan999AndLessThan10000()
    {
        var code = new OneTimeCode(It.IsAny<string>());
        code.Code.Should().BeGreaterThan(999);
        code.Code.Should().BeLessThan(10000);
    }
    
    [Test]
    public void CodeContainsTheSameMobilePhone()
    {
        const string mobilePhone = "123456789";
        var code = new OneTimeCode(mobilePhone);
        code.MobilePhone.Should().Be(mobilePhone);
    }
}