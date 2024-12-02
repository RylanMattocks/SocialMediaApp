using System.Collections.Generic;
namespace DropTablesSocial.Models;

public class User
{
	public int UserId { get; set; } // Primary Key
	public string Username { get; set; }
	public string Email { get; set; }
	public string ProfileImageUrl {get; set; }
	
	// Navigation properties
	public ICollection<Post> Posts { get; set; } = new List<Post>(); // Posts created by the user
	public ICollection<User> Followers { get; set; } = new List<User>(); // Users who follow this user
	public ICollection<User> Following { get; set; } = new List<User>(); // Users this user follows
	public ICollection<Post> Likes { get; set; } = new List<Post>(); // Posts liked by the user
}
