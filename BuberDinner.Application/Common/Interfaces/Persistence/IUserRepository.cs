namespace BuberDinner.Application.Common.Interfaces.Persistence;

using BuberDinner.Domain.Entites;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}