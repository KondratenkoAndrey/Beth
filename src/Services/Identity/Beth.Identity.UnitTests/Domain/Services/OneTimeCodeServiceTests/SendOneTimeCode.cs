using System;
using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Beth.Identity.UnitTests.Domain.Services.OneTimeCodeServiceTests;

[TestFixture]
public class SendOneTimeCode
{
    private readonly Mock<IOneTimeCodeRepository> _oneTimeCodeRepository;
    private readonly Mock<IOneTimeCodeSender> _oneTimeCodeSender;
    private readonly IOptions<OneTimeCodeSettings> _oneTimeCodeSettings;

    public SendOneTimeCode()
    {
        _oneTimeCodeRepository = new Mock<IOneTimeCodeRepository>();
        _oneTimeCodeSender = new Mock<IOneTimeCodeSender>();
        _oneTimeCodeSettings = Options.Create(new OneTimeCodeSettings { Duration = TimeSpan.FromMinutes(1) });
    }

    [SetUp]
    public void Setup()
    {
        _oneTimeCodeSender.Reset();
        _oneTimeCodeRepository.Reset();
    }

    [Test]
    public async Task IfActiveCodeNotFoundShouldCreateSendAndReturnNewCode()
    {
        var mobilePhone = "1234567890";
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object, _oneTimeCodeSettings);
        var (code, isNew) = await sender.RequestOneTimeCode(mobilePhone);
        _oneTimeCodeRepository.Verify(r => r.AddCodeAsync(code), Times.Once);
        _oneTimeCodeSender.Verify(s => s.SendAsync(code), Times.Once);
        code.Should().NotBeNull();
        isNew.Should().BeTrue();
    }

    [Test]
    public async Task IfActiveCodeFoundShouldReturnExistsCode()
    {
        var mobilePhone = "1234567890";
        _oneTimeCodeRepository
            .Setup(r => r.FindCodeAsync(mobilePhone))
            .ReturnsAsync(new OneTimeCode(mobilePhone, It.IsAny<TimeSpan>()));
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object, _oneTimeCodeSettings);
        var (code, isNew) = await sender.RequestOneTimeCode(mobilePhone);
        code.Should().NotBeNull();
        code.MobilePhone.Should().Be(mobilePhone);
        isNew.Should().BeFalse();
    }
}