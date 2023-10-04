using Domain.Models;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);
}