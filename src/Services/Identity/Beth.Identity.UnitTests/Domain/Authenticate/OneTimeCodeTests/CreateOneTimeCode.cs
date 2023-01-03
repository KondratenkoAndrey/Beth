using System;
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
        var code = new OneTimeCode(It.IsAny<string>(), It.IsAny<TimeSpan>());
        code.Code.Should().BeGreaterThan(999);
        code.Code.Should().BeLessThan(10000);
    }
    
    [Test]
    public void CodeContainsTheSameMobilePhone()
    {
        const string mobilePhone = "123456789";
        var code = new OneTimeCode(mobilePhone, It.IsAny<TimeSpan>());
        code.MobilePhone.Should().Be(mobilePhone);
    }
    
    [Test]
    public void CodeDurationIsCorrect()
    {
        var duration = TimeSpan.FromMinutes(1);
        var code = new OneTimeCode(It.IsAny<string>(), duration);
        code.Duration.Should().Be(duration);
    }
}