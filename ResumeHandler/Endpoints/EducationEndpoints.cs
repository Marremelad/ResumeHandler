using ResumeHandler.Common;
using ResumeHandler.DTOs.Education;
using ResumeHandler.Services;

namespace ResumeHandler.Endpoints;

public class EducationEndpoints
{
    private const string InvalidOperationMessage = "Invalid operation status";
    
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/educations", async (EducationService educationService) =>
        {
            var response = await educationService.GetAllEducations();

            return response.OperationResult switch
            {
                OperationResult.Success => Results.Ok(response.Value),
                OperationResult.GeneralError => Results.Problem(response.Error, statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
        
        app.MapPost("/add-education", async (EducationService educationService, EducationCreateDto newEducation) =>
        {
            var response = await educationService.CreateEducation(newEducation);

            return response.OperationResult switch
            {
                OperationResult.Success => Results.Ok(response.Value),
                OperationResult.GeneralError => Results.Problem(response.Error, statusCode: 500),
                OperationResult.ValidationError => Results.BadRequest(response.Error),
                OperationResult.NotFound => Results.NotFound(response.Error),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });

        app.MapPut("/update-education", async (EducationService educationService, EducationUpdateDto updatedEducation) =>
        {
            var response = await educationService.UpdateEducation(updatedEducation);

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