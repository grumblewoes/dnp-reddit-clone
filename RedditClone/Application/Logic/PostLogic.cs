using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDAO postDAO;
    private readonly IUserDAO userDAO;

    public PostLogic(IPostDAO postDAO, IUserDAO userDAO)
    {
        this.postDAO = postDAO;
        this.userDAO = userDAO;
    }
    
    public async Task<Post> CreateAsync(PostCreationDTO postToCreate)
    {
        User? user = await userDAO.GetByUsernameAsync(postToCreate.Author);
        if (user == null)
        {
            throw new Exception($"User with the name {postToCreate.Author} was not found.");
        }
        
        Post toCreate = new Post(postToCreate.Author, postToCreate.Title, postToCreate.Body);
        Post created = await postDAO.CreateAsync(toCreate);
        return created;
    }
    // gets posts based on search
    public Task<IEnumerable<Post>> GetBySearchAsync(SearchPostParametersDTO searchPostParameters)
    {
        return postDAO.GetBySearchAsync(searchPostParameters);
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