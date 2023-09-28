namespace Domain.DTOs;

public class UserCreationDTO
{
    public string Username { get; }
    public string Password { get; }

    public UserCreationDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
}