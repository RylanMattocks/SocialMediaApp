using System.Collections.Generic;
namespace DropTablesSocial.Models;

public class Post
{
    public int PostId { get; set; } // Primary Key
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Foreign Key
    public int UserId { get; set; } // The user who created this post
    
    // Navigation properties
    public User User { get; set; } // The user who created the post
    public ICollection<User> Likes { get; set; } = new List<User>(); // Users who liked this post
}