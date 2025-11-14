namespace WariSmart.API.IAM.Interfaces.REST.Resources;

public record UserResource(
    int IdUser,
    string Username,
    string Role,
    bool IsAdmin
);
