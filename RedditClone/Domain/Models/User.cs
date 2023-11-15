using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    
    public string NickName { get; set; }
    
    [JsonIgnore] //can't serialize to json if there are circular dependencies, so skip it
    public List<Post> Posts { get; set; }

    public User(string username, string password)
    {
        this.Username = username;
        this.Password = password;
        NickName = "";
        Posts = new List<Post>();
    }

    private User() { }
}