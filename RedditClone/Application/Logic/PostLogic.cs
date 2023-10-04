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
    
    public Task<Post> CreateAsync(PostCreationDTO postToCreate)
    {
        Post toCreate = new Post {
            Title = postToCreate.Title,
            Author = postToCreate.Author,
            Body = postToCreate.Body
        };
    }
}