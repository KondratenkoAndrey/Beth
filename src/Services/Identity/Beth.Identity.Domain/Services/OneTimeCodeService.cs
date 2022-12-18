using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;

namespace Beth.Identity.Domain.Services;

public class OneTimeCodeService : IOneTimeCodeService
{
    private readonly IOneTimeCodeRepository _oneTimeCodeRepository;
    private readonly IOneTimeCodeSender _oneTimeCodeSender;

    public OneTimeCodeService(
        IOneTimeCodeRepository oneTimeCodeRepository,
        IOneTimeCodeSender oneTimeCodeSender)
    {
        _oneTimeCodeRepository = oneTimeCodeRepository;
        _oneTimeCodeSender = oneTimeCodeSender;
    }

    public async Task<(OneTimeCode, bool)> SendOneTimeCode(string mobilePhone)
    {
        var code = await _oneTimeCodeRepository.FindActiveCodeAsync(mobilePhone);
        if (code != null)
        {
            return (code, false);
        }

        code = new OneTimeCode(mobilePhone);
        await _oneTimeCodeRepository.AddCodeAsync(code);
        await _oneTimeCodeSender.SendAsync(code);

        return (code, true);
    }
}