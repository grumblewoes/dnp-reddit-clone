namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public Post(string author, string title, string body)
    {
        //post ID gets generated in DAO file
        Author = author;
        Title = title;
        Body = body;
    }
}