using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

    
    [HttpPost, Route("Create"),  Authorize]
    public async Task<ActionResult> CreatePostAsync([FromBody] PostCreationDTO postToBeCreated)
    {
        try
        {
            await postLogic.CreateAsync(postToBeCreated);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost, Route("Post")]
    public async Task<ActionResult> getAsync([FromBody] SelectedPostDto selectedPost)
    {
        try
        {
            PostFullDto post = await postLogic.GetAsync(selectedPost);
            return Ok(post);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
    
    [HttpGet, Route("Posts")]
    public async Task<ActionResult> GetAllPosts()
    {
        try
        {
            return Ok(await postLogic.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    [HttpGet("id")]
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