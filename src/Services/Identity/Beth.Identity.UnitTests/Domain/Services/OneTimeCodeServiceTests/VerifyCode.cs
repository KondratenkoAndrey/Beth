using System;
using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.Services;
using Beth.SharedKernel.EventBus.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Beth.Identity.UnitTests.Domain.Services.OneTimeCodeServiceTests;

public class VerifyCode
{
    
    private readonly Mock<IOneTimeCodeRepository> _oneTimeCodeRepository;
    private readonly IOptions<OneTimeCodeSettings> _oneTimeCodeSettings;
    private readonly Mock<IEventBus> _eventBus;

    public VerifyCode()
    {
        _oneTimeCodeRepository = new Mock<IOneTimeCodeRepository>();
        _oneTimeCodeSettings = Options.Create(new OneTimeCodeSettings { Duration = TimeSpan.FromMinutes(1) });
        _eventBus = new Mock<IEventBus>();
    }

    [Test]
    public async Task ReturnTrueIfCodeIsExistAndCorrect()
    {
        var mobilePhone = "1234567890";
        var oneTimeCode = new OneTimeCode(mobilePhone, _oneTimeCodeSettings.Value.Duration);
        _oneTimeCodeRepository
            .Setup(r => r.FindCodeAsync(mobilePhone))
            .ReturnsAsync(oneTimeCode);
        var service = new OneTimeCodeService(_oneTimeCodeRepository.Object, _eventBus.Object, _oneTimeCodeSettings);
        
        var result = await service.VerifyCodeAsync(mobilePhone, oneTimeCode.Code);

        result.Should().BeTrue();
    }

    [Test]
    public async Task ReturnFalseIfCodeNotExists()
    {
        // _oneTimeCodeRepository
        //     .Setup(r => r.FindCodeAsync(It.IsAny<string>()))
        //     .ReturnsAsync((OneTimeCode)null);
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _eventBus.Object, _oneTimeCodeSettings);
        var result = await sender.VerifyCodeAsync(It.IsAny<string>(), It.IsAny<int>());
        result.Should().BeFalse();
    }
    
    [Test]
    public async Task ReturnFalseIfCodeExistsButWrong()
    {
        var mobilePhone = "1234567890";
        var oneTimeCode = new OneTimeCode(mobilePhone, _oneTimeCodeSettings.Value.Duration);
        _oneTimeCodeRepository
            .Setup(r => r.FindCodeAsync(mobilePhone))
            .ReturnsAsync(oneTimeCode);
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _eventBus.Object, _oneTimeCodeSettings);
        var result = await sender.VerifyCodeAsync(mobilePhone, oneTimeCode.Code + 1);
        result.Should().BeFalse();
    }
}