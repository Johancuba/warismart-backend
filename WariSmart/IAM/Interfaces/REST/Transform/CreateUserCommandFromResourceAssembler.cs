using WariSmart.API.IAM.Domain.Model.Commands;
using WariSmart.API.IAM.Interfaces.REST.Resources;

namespace WariSmart.API.IAM.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource) =>
        new CreateUserCommand(resource.Username, resource.Password, resource.Role);
}
