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

public class FindOneTimeCode
{
    
    private readonly Mock<IOneTimeCodeRepository> _oneTimeCodeRepository;
    private readonly Mock<IOneTimeCodeSender> _oneTimeCodeSender;
    private readonly IOptions<OneTimeCodeSettings> _oneTimeCodeSettings;

    public FindOneTimeCode()
    {
        _oneTimeCodeRepository = new Mock<IOneTimeCodeRepository>();
        _oneTimeCodeSender = new Mock<IOneTimeCodeSender>();
        _oneTimeCodeSettings = Options.Create(new OneTimeCodeSettings { Duration = TimeSpan.FromMinutes(1) });
    }

    [Test]
    public async Task ReturnCodeIfExists()
    {
        var oneTimeCode = new OneTimeCode();
        _oneTimeCodeRepository
            .Setup(r => r.FindCodeAsync(It.IsAny<string>()))
            .ReturnsAsync(oneTimeCode);
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object, _oneTimeCodeSettings);
        var code = await sender.FindOneTimeCodeAsync(It.IsAny<string>());
        code.Should().Be(oneTimeCode);
    }

    [Test]
    public async Task ReturnNullIfCodeNotExists()
    {
        _oneTimeCodeRepository
            .Setup(r => r.FindCodeAsync(It.IsAny<string>()))
            .ReturnsAsync((OneTimeCode)null);
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object, _oneTimeCodeSettings);
        var code = await sender.FindOneTimeCodeAsync(It.IsAny<string>());
        code.Should().BeNull();
    }
}