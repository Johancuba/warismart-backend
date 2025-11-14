namespace WariSmart.API.IAM.Domain.Model.Commands;

public record CreateUserCommand(string Username, string Password, string Role);
