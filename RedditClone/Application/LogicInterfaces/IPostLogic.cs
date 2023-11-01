using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDTO postToCreate);
    Task<IEnumerable<Post>> GetBySearchAsync(SearchPostParametersDTO searchPostParameters);
    Task<IEnumerable<PostBasicDto>> GetAllAsync();
    Task<PostFullDto> GetAsync(SelectedPostDto postDto);
    Task<Post?> GetByIdAsync(int id);
}