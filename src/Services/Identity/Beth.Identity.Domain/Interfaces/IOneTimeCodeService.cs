using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;

namespace Beth.Identity.Domain.Interfaces;

public interface IOneTimeCodeService
{
    public Task<(OneTimeCode, bool)> RequestOneTimeCode(string mobilePhone);
    public Task<OneTimeCode> FindOneTimeCodeAsync(string mobilePhone);
}