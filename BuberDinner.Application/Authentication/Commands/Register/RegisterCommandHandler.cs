namespace BuberDinner.Application.Authentication.Commands.Register;

using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entites;
using ErrorOr;
using MediatR;

public class RegisterCommandHandler : 
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.userRepository = userRepository;
        this.jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // validate user doesnt exist
        if (userRepository.GetUserByEmail(command.email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
       
        //create new user
        var user = new User
        {
            FirstName = command.firstName,
            LastName = command.lastName,
            Email = command.email,
            Password = command.password
        };
        
        userRepository.Add(user);
        
        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}