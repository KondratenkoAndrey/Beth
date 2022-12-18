using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;

namespace Beth.Identity.Infrastructure.Repositories;

public class FakeOneTimeCodeRepository : IOneTimeCodeRepository
{
    private static IList<OneTimeCode>? _codes;

    public FakeOneTimeCodeRepository()
    {
        if (_codes == null)
        {
            _codes = new List<OneTimeCode>();
        }
    }

    public async Task<OneTimeCode?> FindActiveCodeAsync(string mobilePhone)
    {
        var code = _codes?.SingleOrDefault(c => c.MobilePhone == mobilePhone && c.ExpiredAt > DateTime.Now);
        return await Task.FromResult(code);
    }

    public async Task AddCodeAsync(OneTimeCode code)
    {
        _codes?.Add(code);
        await Task.CompletedTask;
    }
}