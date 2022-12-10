namespace Beth.Identity.Application.Models;

public class UserModel
{
    /// <summary>
    /// Мобильный телефон
    /// </summary>
    public string MobilePhone { get; set; }
    
    public UserModel(string mobilePhone)
    {
        MobilePhone = mobilePhone;
    }
}