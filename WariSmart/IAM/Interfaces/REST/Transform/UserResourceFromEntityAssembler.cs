using WariSmart.API.IAM.Domain.Model.Aggregates;
using WariSmart.API.IAM.Interfaces.REST.Resources;

namespace WariSmart.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity) =>
        new UserResource(
            entity.IdUser,
            entity.Username,
            entity.Role,
            entity.IsAdmin()
        );
}
