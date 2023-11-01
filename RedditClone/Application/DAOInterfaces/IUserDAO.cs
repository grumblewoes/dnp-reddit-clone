using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    
    Task<User?> GetUser(string username);
}