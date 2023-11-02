using System.Security.Claims;
using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    Task<User> RegisterAsync(UserCreationDTO dto);
    Task LoginAsync(string userName, string password);
    Task LogoutAsync();
    Task<ClaimsPrincipal> GetAuthAsync();
}