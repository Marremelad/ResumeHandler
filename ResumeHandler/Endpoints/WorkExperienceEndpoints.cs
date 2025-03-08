using ResumeHandler.Common;
using ResumeHandler.DTOs.WorkExperience;
using ResumeHandler.Services;

namespace ResumeHandler.Endpoints;

public class WorkExperienceEndpoints
{
    private const string InvalidOperationMessage = "Invalid operation status";
    
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapPost("/add-work-experience", async (WorkExperienceService workExperienceService, WorkExperienceCreateDto newWorkExperience) =>
        {
            var response = await workExperienceService.CreateWorkExperience(newWorkExperience);

            return response.OperationResult switch
            {
                OperationResult.Success => Results.Ok(response.Value),
                OperationResult.GeneralError => Results.Problem(response.Error, statusCode: 500),
                OperationResult.ValidationError => Results.BadRequest(response.Error),
                OperationResult.NotFound => Results.NotFound(response.Error),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
    }
}