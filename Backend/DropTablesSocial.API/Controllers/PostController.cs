using DropTablesSocial.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace DropTablesSocial.API;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase{
    private readonly IPostService _postService;
    public PostController(IPostService postService) {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts() {
        try {
            return Ok(await _postService.GetAllPosts());
        }
        catch {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id) {
        try {
            return Ok(await _postService.GetPostById(id));
        }
        catch {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPost([FromBody] AddPostDTO post) {
        try {
            await _postService.AddPost(post);
            return Created();
        }
        catch {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostDTO post)
    {
        try
        {
            await _postService.UpdatePost(id, post);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id) {
        try {
            await _postService.DeletePost(id);
            return NoContent();
        }
        catch {
            return BadRequest();
        }
    }
}