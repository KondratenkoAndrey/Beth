using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Beth.Identity.Infrastructure;

public class FakeOneTimeCodeSender : IOneTimeCodeSender
{
    private readonly ILogger<FakeOneTimeCodeSender> _logger;

    public FakeOneTimeCodeSender(ILogger<FakeOneTimeCodeSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(OneTimeCode code)
    {
        _logger.LogInformation("Одноразовый код {code} отправлен на {phone}", code.Code, code.MobilePhone);
        return Task.CompletedTask;
    }
}