using Application.DAOInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class PostDAO : IPostDAO
{
    private readonly FileContext context;

    public PostDAO(FileContext context)
    {
        this.context = context;
    }
    
    //generates post id
    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;
        
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }
}