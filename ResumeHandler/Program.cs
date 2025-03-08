using Microsoft.EntityFrameworkCore;
using ResumeHandler.Data;
using ResumeHandler.Endpoints;
using ResumeHandler.Models;
using ResumeHandler.Services;

namespace ResumeHandler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ResumeHandlerDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<EducationService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        UserEndpoint.RegisterEndpoints(app);
        EducationEndpoint.RegisterEndpoints(app);
        
        app.Run();
    }
}