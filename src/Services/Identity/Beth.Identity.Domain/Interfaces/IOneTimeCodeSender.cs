using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;

namespace Beth.Identity.Domain.Interfaces;

public interface IOneTimeCodeSender
{
    public Task SendAsync(OneTimeCode code, string mobilePhone);
}