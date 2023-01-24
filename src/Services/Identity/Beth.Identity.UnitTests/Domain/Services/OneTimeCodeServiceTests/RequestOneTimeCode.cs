using System;
using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.Services;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBus.Commands;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Beth.Identity.UnitTests.Domain.Services.OneTimeCodeServiceTests;

[TestFixture]
public class SendOneTimeCode
{
    private readonly Mock<IOneTimeCodeRepository> _oneTimeCodeRepository;
    private readonly Mock<IOneTimeCodeService> _oneTimeCodeService;
    private readonly Mock<IEventBus> _eventBus;
    private readonly IOptions<OneTimeCodeSettings> _oneTimeCodeSettings;

    public SendOneTimeCode()
    {
        _oneTimeCodeRepository = new Mock<IOneTimeCodeRepository>();
        _oneTimeCodeService = new Mock<IOneTimeCodeService>();
        _eventBus = new Mock<IEventBus>();
        _oneTimeCodeSettings = Options.Create(new OneTimeCodeSettings { Duration = TimeSpan.FromMinutes(1) });
    }

    [SetUp]
    public void Setup()
    {
        _oneTimeCodeService.Reset();
        _oneTimeCodeRepository.Reset();
        _eventBus.Reset();
    }

    [Test]
    public async Task IfActiveCodeNotFoundShouldCreateNewCode()
    {
        var mobilePhone = "1234567890";
        var service = new OneTimeCodeService(_oneTimeCodeRepository.Object, _eventBus.Object, _oneTimeCodeSettings);
        
        var (code, isNew) = await service.RequestOneTimeCode(mobilePhone);
        
        _oneTimeCodeRepository.Verify(r => r.AddCodeAsync(code), Times.Once);
        _eventBus.Verify(b => b.SendAsync(It.IsAny<SendOneTimeCodeCommand>()), Times.Once);
        code.Should().NotBeNull();
        code.MobilePhone.Should().Be(mobilePhone);
        isNew.Should().BeTrue();
    }

    [Test]
    public async Task IfActiveCodeFoundShouldReturnExistsCode()
    {
        var mobilePhone = "1234567890";
        _oneTimeCodeRepository
            .Setup(r => r.FindCodeAsync(mobilePhone))
            .ReturnsAsync(new OneTimeCode(mobilePhone, It.IsAny<TimeSpan>()));
        var service = new OneTimeCodeService(_oneTimeCodeRepository.Object, _eventBus.Object, _oneTimeCodeSettings);
        
        var (code, isNew) = await service.RequestOneTimeCode(mobilePhone);
        
        code.Should().NotBeNull();
        code.MobilePhone.Should().Be(mobilePhone);
        isNew.Should().BeFalse();
    }
}