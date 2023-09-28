using Domain.Models;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task<User?> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}