using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDTO postToCreate);
    Task<IEnumerable<Post>> GetBySearchAsync(SearchPostParametersDTO searchPostParameters);
    Task<IEnumerable<Post>> GetAllPosts();

    Task<Post?> GetByIdAsync(int id);
}