namespace Domain.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Post> Posts { get; set; }
}