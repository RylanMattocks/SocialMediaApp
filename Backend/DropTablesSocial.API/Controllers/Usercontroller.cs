using DropTablesSocial.Models;
using Microsoft.AspNetCore.Mvc;

namespace DropTablesSocial.API;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase{
    private readonly IUserService _userService;
    public UserController(IUserService userService) {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers() {
        try {
            return Ok(await _userService.GetAllUsers());
        }
        catch {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id) {
        try {
            return Ok(await _userService.GetUserById(id));
        }
        catch {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserDTO user) {
        try {
            await _userService.AddUser(user);
            return Created();
        }
        catch {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO user)
    {
        try
        {
            await _userService.UpdateUser(id, user);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) {
        try {
            await _userService.DeleteUser(id);
            return NoContent();
        }
        catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPost("{id}/post/{postId}")]
    public async Task<IActionResult> LikePost(int id, int postId) {
        try {
            await _userService.LikePost(id, postId);
            return NoContent();
        }
        catch {
            return BadRequest();
        }
    }

    [HttpDelete("{id}/post/{postId}")]
    public async Task<IActionResult> UnLikePost(int id, int postId) {
        try {
            await _userService.UnLikePost(id, postId);
            return NoContent();
        }
        catch {
            return BadRequest();
        }
    }

    [HttpPost("{followerId}/user/{followeeId}")]
    public async Task<IActionResult> FollowUser(int followerId, int followeeId) {
        try {
            await _userService.FollowUser(followerId, followeeId);
            return NoContent();
        }
        catch {
            return BadRequest();
        }
    }

    [HttpDelete("{followerId}/user/{followeeId}")]
    public async Task<IActionResult> UnfollowUser(int followerId, int followeeId) {
        try {
            await _userService.UnFollowUser(followerId, followeeId);
            return NoContent();
        }
        catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpGet("login/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username) {
        try {
            return Ok(await _userService.GetUserByUsername(username));
        }
        catch {
            return BadRequest();
        }
    }
}