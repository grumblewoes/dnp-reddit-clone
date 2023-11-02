using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<Post> GetPostAsync(int postId);
    Task<ICollection<Post>> GetAsync(
        string? description, 
        string? username, 
        string? titleContains,
        int? parentId
    );
    Task<Post> CreateAsync(PostCreationDTO dto);
}