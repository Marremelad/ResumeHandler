using ResumeHandler.Common;
using ResumeHandler.Services;

namespace ResumeHandler.Endpoints;

public class UserEndpoints
{
    private const string InvalidOperationMessage = "Invalid operation status";

    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/users", async (UserService userService) =>
        {
            var response = await userService.GetAllUsersAsync();

            return response.OperationResult switch
            {
                OperationResult.Success => Results.Ok(response.Value),
                OperationResult.GeneralError => Results.Problem(response.Error, statusCode: 500),
                _ => Results.Problem(InvalidOperationMessage)
            };
        });
        
        app.MapGet("/users/{id:int}", async (UserService userService, int id) =>
        {
            var response = await userService.GetUserAsync(id);

            return response.OperationResult switch
            {
                OperationResult.Success => Results.Ok(response.Value),
                OperationResult.GeneralError => Results.Problem(response.Error, statusCode: 500),
                OperationResult.NotFound => Results.NotFound(response.Error),
                _ => Results.Problem(InvalidOperationMessage)
            };
        });
    }
}