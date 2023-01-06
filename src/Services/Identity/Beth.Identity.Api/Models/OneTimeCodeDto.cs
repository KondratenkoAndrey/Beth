using Beth.Identity.Domain.Authenticate;

namespace Beth.Identity.Api.Models;

public record OneTimeCodeDto
{
    public bool IsNew { get; }
    public DateTime ExpiredAt { get; }
    public OneTimeCodeDto(OneTimeCode code, bool isNew)
    {
        IsNew = isNew;
        ExpiredAt = code.CreatedAt.Add(code.Duration);
    }
}