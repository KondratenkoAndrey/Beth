using System;

namespace Beth.Identity.Domain.Authenticate;

public class OneTimeCode
{
    public int Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan Duration { get; set; }
    public string MobilePhone { get; set; }

    public OneTimeCode()
    {
    }
    
    public OneTimeCode(string mobilePhone, TimeSpan duration)
    {
        Code = new Random().Next(1000, 9999);
        CreatedAt = DateTime.UtcNow;
        Duration = duration;
        MobilePhone = mobilePhone;
    }
}