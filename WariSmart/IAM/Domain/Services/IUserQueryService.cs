using WariSmart.API.IAM.Domain.Model.Aggregates;
using WariSmart.API.IAM.Domain.Model.Queries;

namespace WariSmart.API.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
}
