using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Beth.Identity.Domain.Services;

public class OneTimeCodeService : IOneTimeCodeService
{
    private readonly IOneTimeCodeRepository _oneTimeCodeRepository;
    private readonly IOneTimeCodeSender _oneTimeCodeSender;
    private readonly OneTimeCodeSettings _settings;

    public OneTimeCodeService(
        IOneTimeCodeRepository oneTimeCodeRepository,
        IOneTimeCodeSender oneTimeCodeSender,
        IOptions<OneTimeCodeSettings> settings)
    {
        _oneTimeCodeRepository = oneTimeCodeRepository;
        _oneTimeCodeSender = oneTimeCodeSender;
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
        await _oneTimeCodeSender.SendAsync(code);

        return (code, true);
    }

    public async Task<OneTimeCode> FindOneTimeCodeAsync(string mobilePhone)
    {
        return await _oneTimeCodeRepository.FindCodeAsync(mobilePhone);
    }
}