namespace BuberDinner.Infrastructure.Persistence;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entites;

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = new();
    
    public User? GetUserByEmail(string email)
    {
        return users.SingleOrDefault(x => x.Email == email);
    }

    public void Add(User user)
    {
        users.Add(user);
    }
}