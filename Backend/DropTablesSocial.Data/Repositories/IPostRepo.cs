using DropTablesSocial.Models;

namespace DropTablesSocial.Data;

public interface IPostRepo {
    public Task<IEnumerable<Post>> GetAllPosts();
    public Task<Post> GetPostById(int id);
    public Task AddPost(Post post);
    public Task UpdatePost(Post post);
    public Task DeletePost(Post post);
}