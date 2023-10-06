using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController: ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostsController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync(PostCreationDTO dto)
    {
        try
        {
            Post post = await postLogic.CreateAsync(dto);
            return Created($"/post/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? author,
        [FromQuery] int? postId, [FromQuery] string? titleContains)
    {
        try
        {
            SearchPostParametersDto parameters = new SearchPostParametersDto(author, postId, titleContains);
            var posts = await postLogic.GetAsync(parameters);
            if (!posts.Any())
            {
                return NotFound("No posts found matching the search criteria.");
            }
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        try
        {
            var posts = await postLogic.GetAllPosts();
            if (!posts.Any())
            {
                return NotFound("No posts found.");
            }
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}