using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.UserAggregate;

namespace Beth.Identity.Domain.Interfaces;

public interface IOneTimeCodeSender
{
    public Task SendAsync(OneTimeCode code, User user);
}