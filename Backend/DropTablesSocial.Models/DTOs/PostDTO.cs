namespace DropTablesSocial.Models;

public class PostDTO {
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<UserInfoDTO> Likes { get; set; } = new List<UserInfoDTO>();
}