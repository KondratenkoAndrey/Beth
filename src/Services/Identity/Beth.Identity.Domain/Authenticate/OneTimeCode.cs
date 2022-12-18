using System;

namespace Beth.Identity.Domain.Authenticate;

public class OneTimeCode
{
    public int Code { get; }
    public DateTime ExpiredAt { get; }
    public string MobilePhone { get; }

    public OneTimeCode(string mobilePhone)
    {
        Code = new Random().Next(1000, 9999);
        ExpiredAt = DateTime.Now.AddMinutes(1);
        MobilePhone = mobilePhone;
    }
}