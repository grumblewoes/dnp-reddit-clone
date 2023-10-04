namespace Domain.DTOs;

public class PostCreationDTO
{
    public string Title { get; }
    public string Author { get; }
    public string Body { get; }

    public PostCreationDTO(string title, string author, string body)
    {
        Author = author;
        Title = title;
        Body = body;
    }
    
}