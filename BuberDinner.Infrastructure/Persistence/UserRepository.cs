namespace BuberDinner.Infrastructure.Persistence;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entites;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = [];
    
    public User? GetUserByEmail(string email)
    {
        return Users.SingleOrDefault(x => x.Email == email);
    }

    public void Add(User user)
    {
        Users.Add(user);
    }
}