using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Beth.Identity.UnitTests.Domain.Services.OneTimeCodeServiceTests;

[TestFixture]
public class SendOneTimeCode
{
    private readonly Mock<IOneTimeCodeRepository> _oneTimeCodeRepository;
    private readonly Mock<IOneTimeCodeSender> _oneTimeCodeSender;

    public SendOneTimeCode()
    {
        _oneTimeCodeRepository = new Mock<IOneTimeCodeRepository>();
        _oneTimeCodeSender = new Mock<IOneTimeCodeSender>();
    }

    [SetUp]
    public void Setup()
    {
        _oneTimeCodeSender.Reset();
        _oneTimeCodeRepository.Reset();
    }

    [Test]
    public async Task InvokeSendServiceSendOneTimeCodeOnce()
    {
        var mobilePhone = "1234567890";
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object);
        await sender.SendOneTimeCode(mobilePhone);
        _oneTimeCodeSender.Verify(x => x.SendAsync(It.IsAny<OneTimeCode>(), mobilePhone), Times.Once);
    }

    [Test]
    public async Task IfNewCodeGeneratedShouldInvokeOneTimeCodeRepositoryAddCodeAsyncOnce()
    {
        var mobilePhone = "1234567890";
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object);
        await sender.SendOneTimeCode(mobilePhone);
        _oneTimeCodeRepository.Verify(x => x.AddCodeAsync(It.IsAny<OneTimeCode>()), Times.Once);
    }

    [Test]
    public async Task ReturnIsNewFlagIfActiveCodeNotFound()
    {
        var mobilePhone = "1234567890";
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object);
        var (_, isNew) = await sender.SendOneTimeCode(mobilePhone);
        isNew.Should().BeTrue();
    }

    [Test]
    public async Task ReturnNotIsNewFlagIfActiveCodeFound()
    {
        var mobilePhone = "1234567890";
        _oneTimeCodeRepository
            .Setup(r => r.FindActiveCodeAsync(mobilePhone))
            .ReturnsAsync(new OneTimeCode(mobilePhone));
        var sender = new OneTimeCodeService(_oneTimeCodeRepository.Object, _oneTimeCodeSender.Object);
        var (_, isNew) = await sender.SendOneTimeCode(mobilePhone);
        isNew.Should().BeFalse();
    }
}