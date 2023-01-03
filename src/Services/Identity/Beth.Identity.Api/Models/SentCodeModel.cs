using Beth.Identity.Domain.Authenticate;

namespace Beth.Identity.Api.Models;

public class SentCodeModel
{
    public bool IsNew { get; }
    public DateTime ExpiredAt { get; }
    public SentCodeModel(OneTimeCode code, bool isNew)
    {
        IsNew = isNew;
        ExpiredAt = code.CreatedAt.Add(code.Duration);
    }
}