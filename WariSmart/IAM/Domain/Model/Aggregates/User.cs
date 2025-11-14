using WariSmart.API.IAM.Domain.Model.Commands;

namespace WariSmart.API.IAM.Domain.Model.Aggregates;

/// <summary>
/// User Aggregate
/// Represents a user in the system (Worker or Admin)
/// </summary>
public partial class User
{
    protected User()
    {
        Username = string.Empty;
        Password = string.Empty;
        Role = string.Empty;
    }

    public User(CreateUserCommand command)
    {
        Username = command.Username;
        Password = command.Password; // In production, this should be hashed
        Role = command.Role;
    }

    public int IdUser { get; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; } // "Admin" or "Worker"

    public bool IsAdmin() => Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
}
