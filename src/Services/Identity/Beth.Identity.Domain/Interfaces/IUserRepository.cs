using System.Threading.Tasks;
using Beth.Identity.Domain.UserAggregate;

namespace Beth.Identity.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User> FindUserAsync(string mobilePhone);
    public Task AddUserAsync(User user);
}