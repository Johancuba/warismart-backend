using WariSmart.API.IAM.Domain.Model.Aggregates;
using WariSmart.API.IAM.Domain.Model.Commands;

namespace WariSmart.API.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand command);
}
