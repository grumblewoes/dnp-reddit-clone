using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDAO postDAO;

    public PostLogic(IPostDAO postDAO)
    {
        this.postDAO = postDAO;
    }
    
    public async Task<Post> CreateAsync(PostCreationDTO postToCreate)
    {
        Post post = new Post()
        {
            Title= postToCreate.Title,
            Body = postToCreate.Body,
            Author= postToCreate.NickName,
        };

        Post createdPost = await postDAO.CreateAsync(post);

        return createdPost;
    }
    // gets posts based on search
    public Task<IEnumerable<Post>> GetBySearchAsync(SearchPostParametersDTO searchPostParameters)
    {
        return postDAO.GetBySearchAsync(searchPostParameters);
    }

    public async Task<IEnumerable<PostBasicDto>> GetAllAsync()
    {
        IEnumerable<Post>? posts = await postDAO.GetAllAsync();


        List<PostBasicDto>? postFullDtos = new List<PostBasicDto>();
        foreach (var post in posts)
        {
            postFullDtos.Add(new PostBasicDto
            {
                PostTitle = post.Title,
                index = post.Id
            });
        }
        return postFullDtos;
    }

    public async Task<PostFullDto> GetAsync(SelectedPostDto postDto)
    {
        Post? redditpost = await postDAO.GetByIdAsync(postDto.id);

        return new PostFullDto()
        {
            PostTitle = redditpost.Title,
            PostContext = redditpost.Body,
            PostCreator = redditpost.Author,
            index = redditpost.Id
        };
    }

    //gets all posts
    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        return await postDAO.GetAllAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await postDAO.GetByIdAsync(id);
    }
}