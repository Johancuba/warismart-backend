using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WariSmart.API.IAM.Domain.Model.Queries;
using WariSmart.API.IAM.Domain.Services;
using WariSmart.API.IAM.Interfaces.REST.Resources;
using WariSmart.API.IAM.Interfaces.REST.Transform;

namespace WariSmart.API.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Users")]
public class UsersController(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new user", OperationId = "CreateUser")]
    [SwaggerResponse(201, "User created", typeof(UserResource))]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        try
        {
            var result = await userCommandService.Handle(command);
            if (result is null) return BadRequest("Failed to create user");
            
            return CreatedAtAction(nameof(GetUserById), new { id = result.IdUser }, 
                UserResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception ex) when (ex.Message.Contains("already exists"))
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Gets all users", OperationId = "GetAllUsers")]
    [SwaggerResponse(200, "Users retrieved", typeof(IEnumerable<UserResource>))]
    public async Task<ActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var result = await userQueryService.Handle(query);
        var resources = result.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Gets a user by ID", OperationId = "GetUserById")]
    [SwaggerResponse(200, "User found", typeof(UserResource))]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult> GetUserById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await userQueryService.Handle(query);
        
        if (result is null) return NotFound();
        
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}
