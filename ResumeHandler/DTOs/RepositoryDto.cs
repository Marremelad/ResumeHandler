using System.Text.Json.Serialization;

namespace ResumeHandler.DTOs;

public class RepositoryDto
{
    public string? Name { get; set; }
    
    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }
    
    public string? Description { get; set; }

    public string? Language { get; set; }
}
