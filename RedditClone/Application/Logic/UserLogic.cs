using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO userDAO;

    public UserLogic(IUserDAO userDAO)
    {
        this.userDAO = userDAO;
    }
    
    public async Task<User> CreateAsync(UserCreationDTO userToCreate)
    {
        //check to see if the user exists. if it does, throw exception.
        User? existing = await userDAO.GetByUsernameAsync(userToCreate.Username);
        if (existing != null)
            throw new Exception("Username is taken.");

        ValidateData(userToCreate);
        User toCreate = new User
        {
            Username = userToCreate.Username,
            Password = userToCreate.Password
        };

        User created = await userDAO.CreateAsync(toCreate);
        return created;
    }

    private void ValidateData(UserCreationDTO userToCreate)
    {
        string username = userToCreate.Username;

        if (username.Length < 3)
            throw new Exception("Username must be at least 3 characters.");
        if (username.Length > 15)
            throw new Exception("Username must be less than 16 characters.");
    }
}