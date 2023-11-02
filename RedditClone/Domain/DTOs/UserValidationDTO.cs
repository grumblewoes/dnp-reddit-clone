namespace Domain.DTOs;

public class UserValidationDTO
{
    public string Username { get; }
    public string Password { get; }

    public UserValidationDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
}