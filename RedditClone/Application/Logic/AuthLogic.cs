using System.ComponentModel.DataAnnotations;
using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class AuthLogic : IAuthLogic
{
    private readonly IUserDAO userDAO;

    public AuthLogic(IUserDAO userDAO)
    {
        this.userDAO = userDAO;
    }

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = userDAO.GetByUsernameAsync(username).Result;
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task LoginAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }
}