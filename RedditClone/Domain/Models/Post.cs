namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User Author { get; }
    public string Title { get; }
    public string Body { get; }

    public Post(User author, string title, string body)
    {
        Author = author;
        Title = title;
        Body = body;
    }
}