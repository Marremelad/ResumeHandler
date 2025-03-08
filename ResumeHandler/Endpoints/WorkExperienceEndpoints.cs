using ResumeHandler.Common;
using ResumeHandler.DTOs.WorkExperience;
using ResumeHandler.Services;

namespace ResumeHandler.Endpoints;

public class WorkExperienceEndpoints
{
    private const string InvalidOperationMessage = "Invalid operation status";
    
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/work-experiences", async (WorkExperienceService workExperienceService) =>
        {
            var response = await workExperienceService.GetAllWorkExperiences();

            return response.OperationResult switch
            {
                OperationResult.Success => Results.Ok(response.Value),
                OperationResult.GeneralError => Results.Problem(response.Error, statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });

        app.MapGet("/work-experiences/{id:int}", async (WorkExperienceService workExperienceService, int id) =>
        {
            var response = await workExperienceService.GetWorkExperience(id);

            return response.OperationResult switch
            {
                OperationResult.Success => Results.Ok(response.Value),
                OperationResult.GeneralError => Results.Problem(response.Error, statusCode: 500),
                OperationResult.NotFound => Results.NotFound(response.Error),
                _ => Results.Problem(InvalidOperationMessage) // Change for all other endpoints.
            };
        });
        
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

        app.MapPut("/update-work-experience", async (WorkExperienceService workExperienceService, WorkExperienceUpdateDto updatedWorkExperience) =>
        {
            var response = await workExperienceService.UpdateWorkExperience(updatedWorkExperience);

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