using WariSmart.API.IAM.Domain.Model.Aggregates;
using WariSmart.API.IAM.Domain.Model.Commands;
using WariSmart.API.IAM.Domain.Repositories;
using WariSmart.API.IAM.Domain.Services;
using CatchUpPlatform.API.Shared.Domain.Repositories;

namespace WariSmart.API.IAM.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IUserCommandService
{
    public async Task<User?> Handle(CreateUserCommand command)
    {
        if (await userRepository.ExistsByUsernameAsync(command.Username))
            throw new Exception($"User with username {command.Username} already exists");

        var user = new User(command);
        
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating user: {e.Message}");
            return null;
        }
    }
}
