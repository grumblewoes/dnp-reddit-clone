namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get;  }
    public int? PostId { get;  }
    public string? TitleContains { get; }

    public SearchPostParametersDto(string? username, int? postId, string? titleContains)
    {
        Username = username;
        PostId= postId;
        TitleContains = titleContains;
    }
}