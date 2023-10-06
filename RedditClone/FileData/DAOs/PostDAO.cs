using System.Globalization;
using Application.DAOInterfaces;
using Domain.DTOs;
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
    //gets all posts searching by author, authorId or Title
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> posts = context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            //author is unique
            posts = context.Posts.Where(p =>
                p.Author.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
            
        }
        if (searchParameters.PostId != null)
        {
            posts = posts.Where(p => p.Id == searchParameters.PostId);
        }

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            posts = posts.Where(p =>
                p.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(posts);
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        IEnumerable<Post> allPosts = context.Posts.ToList();
        return Task.FromResult(allPosts);
    }
}