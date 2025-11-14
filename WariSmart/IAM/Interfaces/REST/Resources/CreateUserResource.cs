using System.ComponentModel.DataAnnotations;

namespace WariSmart.API.IAM.Interfaces.REST.Resources;

public record CreateUserResource(
    [Required] string Username,
    [Required] string Password,
    [Required] string Role // "Admin" or "Worker"
);
