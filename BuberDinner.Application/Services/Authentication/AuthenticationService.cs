using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entites;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // make sure exists
        if (userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email does not exist");
        }
        
        // validate password
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }
        
        // create jwt
        var token = jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // validate user doesnt exist
        if (userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User with given email already exists");
        }
       
        //create new user
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        
        userRepository.Add(user);
        
        var token = jwtTokenGenerator.GenerateToken(user);


        return new AuthenticationResult(user, token);
    }
}