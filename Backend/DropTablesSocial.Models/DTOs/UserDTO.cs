namespace DropTablesSocial.Models;

public class UserDTO {
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    public ICollection<PostDTO> Posts { get; set; } = new List<PostDTO>();
    public ICollection<UserInfoDTO> Followers { get; set; } = new List<UserInfoDTO>();
    public ICollection<UserInfoDTO> Following { get; set; } = new List<UserInfoDTO>();
    public ICollection<PostDTO> Likes { get; set; } = new List<PostDTO>();
}

public class UserInfoDTO {
    public int UserID { get; set; }
    public string Username { get; set; }
}