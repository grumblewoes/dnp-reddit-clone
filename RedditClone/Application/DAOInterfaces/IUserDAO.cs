using Domain.Models;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task<User?> CreateAsync(User user);
}