using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBus.Commands;
using Microsoft.Extensions.Logging;

namespace Beth.Identity.Infrastructure;

public class OneTimeCodeSender : IOneTimeCodeSender
{
    private readonly ILogger<OneTimeCodeSender> _logger;
    private readonly IEventBus _eventBus;

    public OneTimeCodeSender(ILogger<OneTimeCodeSender> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task SendAsync(OneTimeCode code)
    {
        var command = new SendOneTimeCodeCommand(code.Code, code.MobilePhone);
        await _eventBus.SendAsync(command);
        _logger.LogInformation("{command}", command);
    }
}