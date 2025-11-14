using WariSmart.API.IAM.Domain.Model.Aggregates;
using WariSmart.API.IAM.Domain.Model.Queries;
using WariSmart.API.IAM.Domain.Repositories;
using WariSmart.API.IAM.Domain.Services;

namespace WariSmart.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.IdUser);
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
}
