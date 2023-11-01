using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    public Task<ICollection<PostBasicDto>> GetAllAsync();

    public Task CreateAsync(string title, string mainText, string nickName);

    public Task<PostFullDto> GetAsync(int id);
    
}