using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetBySearchAsync(SearchPostParametersDTO searchParameters);
    Task<IEnumerable<Post>> GetAllAsync();

    Task<Post?> GetByIdAsync(int id);
}