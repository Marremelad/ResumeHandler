using System.Text.Json;
using ResumeHandler.DTOs;

namespace ResumeHandler.Endpoints;

public class GitHubEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/github-repos/{username}", async (HttpClient client, string username) => {

            client.DefaultRequestHeaders.UserAgent.ParseAdd("CSharpApp");

            var response = await client.GetAsync($"https://api.github.com/users/{username}/repos");

            if (!response.IsSuccessStatusCode)
            {
                return Results.BadRequest();
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var data = JsonSerializer.Deserialize<List<RepositoryDto>>(json, options);
            
            data?.ForEach(d => { d.Language ??= "No registered language"; 
                d.Description ??= "No description available. Visit the repository to learn more"; });
            
            return Results.Ok(data);
        });
    }
}