using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.SharedKernel.EventBus.Abstractions;
using Beth.SharedKernel.EventBus.Commands;
using Beth.SharedKernel.EventBus.Events;
using Microsoft.Extensions.Options;

namespace Beth.Identity.Domain.Services;

public class OneTimeCodeService : IOneTimeCodeService
{
    private readonly IOneTimeCodeRepository _oneTimeCodeRepository;
    private readonly IEventBus _eventBus;
    private readonly OneTimeCodeSettings _settings;

    public OneTimeCodeService(
        IOneTimeCodeRepository oneTimeCodeRepository,
        IEventBus eventBus,
        IOptions<OneTimeCodeSettings> settings)
    {
        _oneTimeCodeRepository = oneTimeCodeRepository;
        _eventBus = eventBus;
        _settings = settings.Value;
    }

    public async Task<(OneTimeCode, bool)> RequestOneTimeCode(string mobilePhone)
    {
        var code = await _oneTimeCodeRepository.FindCodeAsync(mobilePhone);
        if (code != null)
        {
            return (code, false);
        }

        code = new OneTimeCode(mobilePhone, _settings.Duration);
        await _oneTimeCodeRepository.AddCodeAsync(code);
        var command = new SendOneTimeCodeCommand(code.Code, code.MobilePhone);
        await _eventBus.SendAsync(command);

        return (code, true);
    }

    public async Task<bool> VerifyCodeAsync(string mobilePhone, int code)
    {
        var oneTimeCode = await _oneTimeCodeRepository.FindCodeAsync(mobilePhone);
        if (oneTimeCode == null || oneTimeCode.Code != code)
        {
            return false;
        }
        
        var integrationEvent = new UserLoggedIntegrationEvent(mobilePhone);
        await _eventBus.PublishAsync(integrationEvent);
        return true;
    }
}