namespace Domain.DTOs;

public class SearchPostParametersDTO
{
    public string? Username { get;  }
    public int? PostId { get;  }
    public string? TitleContains { get; }

    public SearchPostParametersDTO(string? username, int? postId, string? titleContains)
    {
        Username = username;
        PostId= postId;
        TitleContains = titleContains;
    }
}