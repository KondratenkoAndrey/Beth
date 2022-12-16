using Beth.Identity.Domain.Interfaces;
using Beth.Identity.Domain.UserAggregate;

namespace Beth.Identity.Infrastructure.Repositories;

public class FakeUserRepository : IUserRepository
{
    private static IList<User>? _users;

    public FakeUserRepository()
    {
        if (_users == null)
        {
            _users = new List<User>();
        }
    }
    
    public Task<User?> FindUserAsync(string mobilePhone)
    {
        return Task.FromResult(_users?.FirstOrDefault(u => u.MobilePhone == mobilePhone));
    }

    public Task AddUserAsync(User user)
    {
        _users?.Add(user);
        return Task.CompletedTask;
    }
}