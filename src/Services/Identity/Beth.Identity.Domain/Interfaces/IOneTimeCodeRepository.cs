using System.Threading.Tasks;
using Beth.Identity.Domain.Authenticate;

namespace Beth.Identity.Domain.Interfaces;

public interface IOneTimeCodeRepository
{
    public Task AddCodeAsync(OneTimeCode code);
    public Task<OneTimeCode> FindCodeAsync(string mobilePhone);
}