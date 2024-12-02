using DropTablesSocial.Models;

namespace DropTablesSocial.API;

public interface IPostService {
    public Task<IEnumerable<PostDTO>> GetAllPosts();
    public Task<PostDTO> GetPostById(int id);
    public Task AddPost(AddPostDTO post);
    public Task UpdatePost(int id, UpdatePostDTO post);
    public Task DeletePost(int id);
}