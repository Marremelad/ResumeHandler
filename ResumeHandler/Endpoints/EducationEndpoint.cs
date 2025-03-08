﻿using ResumeHandler.Common;
using ResumeHandler.DTOs;
using ResumeHandler.Services;

namespace ResumeHandler.Endpoints;

public class EducationEndpoint
{
    private const string InvalidOperationMessage = "Invalid operation status";
    
    public static void RegisterEndpoints(WebApplication app)
    {
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
    }
}