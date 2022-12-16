using System;

namespace Beth.Identity.Domain.UserAggregate;

public class User
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Мобильный телефон
    /// </summary>
    public string MobilePhone { get; }

    public User(string mobilePhone)
    {
        Id = Guid.NewGuid();
        MobilePhone = mobilePhone;
    }
}