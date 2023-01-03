using System;

namespace Beth.Identity.Domain.Authenticate;

public class OneTimeCode
{
    public int Code { get; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan Duration { get; }
    public string MobilePhone { get; }

    public OneTimeCode(string mobilePhone, TimeSpan duration)
    {
        Code = new Random().Next(1000, 9999);
        CreatedAt = DateTime.UtcNow;
        Duration = duration;
        MobilePhone = mobilePhone;
    }
}