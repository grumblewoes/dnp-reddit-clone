using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Post> GetPostAsync(int postId)
    {
        HttpResponseMessage response = await client.GetAsync($"/posts/{postId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return post;
    }

    public async Task<ICollection<Post>> GetAsync(string? description, string? username,
        string? titleContains, int? parentId)
    {
        string query = ConstructQuery(description, username, titleContains, parentId); 
        
        HttpResponseMessage response = await client.GetAsync("/posts"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/posts", dto);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return post;
    }

    private static string ConstructQuery(string? content, string? username, string? titleContains, int? parentId)
    {
        //Posts? title=This%20is%20a%20good%20q & parentSubForum=1 & owner=1 & content=L
        string temp = "";
        if (!string.IsNullOrEmpty(titleContains))
        {
            temp += string.IsNullOrEmpty(temp) ? "?" : "&";
            temp += $"title={titleContains}";
        }

        if (parentId != null)
        {
            temp += string.IsNullOrEmpty(temp) ? "?" : "&";
            temp += $"parentSubForum={parentId}";
        }

        if (!string.IsNullOrEmpty(username))
        {
            temp += string.IsNullOrEmpty(temp) ? "?" : "&";
            temp += $"owner={username}";
        }

        if (!string.IsNullOrEmpty(content))
        {
            temp += $"?content={content}";
        }

        return temp;
    }
}