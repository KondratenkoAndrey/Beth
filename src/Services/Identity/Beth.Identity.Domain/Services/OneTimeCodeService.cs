using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.UserAggregate;

namespace Beth.Identity.Domain.Services;

public class OneTimeCodeService : IOneTimeCodeService
{
    private readonly IUserRepository _userRepository;
    private readonly IOneTimeCodeRepository _oneTimeCodeRepository;
    private readonly IOneTimeCodeSender _oneTimeCodeSender;

    public OneTimeCodeService(
        IUserRepository userRepository,
        IOneTimeCodeRepository oneTimeCodeRepository,
        IOneTimeCodeSender oneTimeCodeSender)
    {
        _userRepository = userRepository;
        _oneTimeCodeRepository = oneTimeCodeRepository;
        _oneTimeCodeSender = oneTimeCodeSender;
    }

    public async Task<(OneTimeCode, bool)> SendOneTimeCode(string mobilePhone)
    {
        var user = await _userRepository.FindUserAsync(mobilePhone);
        if (user == null)
        {
            user = new User(mobilePhone);
            await _userRepository.AddUserAsync(user);
        }

        var code = await _oneTimeCodeRepository.FindActiveCodeAsync(user.Id);
        if (code != null)
        {
            return (code, false);
        }

        code = new OneTimeCode(user.Id);
        await _oneTimeCodeRepository.AddCodeAsync(code);
        await _oneTimeCodeSender.SendAsync(code, user);

        return (code, true);
    }
}