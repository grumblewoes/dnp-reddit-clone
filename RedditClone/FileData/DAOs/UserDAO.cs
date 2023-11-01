using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserDAO : IUserDAO
{
    private readonly FileContext context;

    public UserDAO(FileContext context)
    {
        this.context = context;
    }
    
    public Task<User> CreateAsync(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing =
            context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u => u.Username.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
    public Task<User?> GetUser(string username)
    {
        User? exsting = context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exsting);
    }
}