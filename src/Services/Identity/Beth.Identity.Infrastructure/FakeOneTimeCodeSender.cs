using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.UserAggregate;
using Microsoft.Extensions.Logging;

namespace Beth.Identity.Infrastructure;

public class FakeOneTimeCodeSender : IOneTimeCodeSender
{
    private readonly ILogger<FakeOneTimeCodeSender> _logger;

    public FakeOneTimeCodeSender(ILogger<FakeOneTimeCodeSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(OneTimeCode code, User user)
    {
        _logger.LogInformation("Одноразовый код {code} для пользователя {user} отправлен на {phone}", code.Code, user.Id, user.MobilePhone);
        return Task.CompletedTask;
    }
}