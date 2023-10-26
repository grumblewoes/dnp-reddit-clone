using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
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

    [HttpGet("search"), AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Post>>> GetBySearchAsync([FromQuery] string? author,
        [FromQuery] int? postId, [FromQuery] string? titleContains)
    {
        try
        {
            SearchPostParametersDTO parameters = new SearchPostParametersDTO(author, postId, titleContains);
            var posts = await postLogic.GetBySearchAsync(parameters);
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
    [HttpGet, AllowAnonymous]
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
    
    [HttpGet("id"), AllowAnonymous]
    public async Task<ActionResult<Post>> GetPostById([FromQuery] int id)
    {
        try
        {
            var post = await postLogic.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound("No post with the ID " + id + " were found.");
            }
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}