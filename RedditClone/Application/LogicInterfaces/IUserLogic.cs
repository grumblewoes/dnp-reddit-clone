using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDTO userToCreate);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}