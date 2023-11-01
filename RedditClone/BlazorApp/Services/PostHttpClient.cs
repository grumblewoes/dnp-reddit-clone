using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BlazorApp.Services;
using Domain.DTOs;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient:IPostService
{
    private readonly HttpClient client;
    

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ICollection<PostBasicDto>> GetAllAsync()
    {
        var response = await client.GetAsync("https://localhost:7252/Posts/Posts");
        string postValues = await response.Content.ReadAsStringAsync();
        Console.WriteLine("API Response Content: " + postValues);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("API returned an error: " + postValues);
        }

        PostBasicDto[] posts = JsonSerializer.Deserialize<PostBasicDto[]>(postValues, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return posts;
    }

    public async Task CreateAsync(string title, string body, string nickname)
    {
         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        
        // string postAsJson = JsonSerializer.Serialize(CreatePostDto(title, body, nickname));
        // StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        // HttpResponseMessage response = await client.PostAsync("/Posts/Create", content);

        PostCreationDTO postToCreate = new PostCreationDTO()
        {
            Title = title, Body = body, NickName = nickname

        };

        HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7252/Posts/Create", postToCreate);
        Console.WriteLine($"Response status code: {response.StatusCode}");
        string responseContent = await response.Content.ReadAsStringAsync(); 
        Console.WriteLine($"Response content: {responseContent}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<PostFullDto> GetAsync(int id)
    {
        string postAsJson = JsonSerializer.Serialize(CreateSelectedPostDto(id));
        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7252/Posts/Post",content);
        string postValues = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(postValues);
        }

        PostFullDto post = JsonSerializer.Deserialize<PostFullDto>(postValues, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return post;
    }
    
    private PostCreationDTO CreatePostDto(string title, string mainText, string nickName)
    {
        PostCreationDTO postToCreate = new PostCreationDTO
        {
            Title = title,
            Body = mainText,
            NickName= nickName
        };
        return postToCreate;
    }

    private SelectedPostDto CreateSelectedPostDto(int id)
    {
        SelectedPostDto postDto = new SelectedPostDto
        {
            id = id
        };
        return postDto;
    }
}
