using DropTablesSocial.Models;
using Microsoft.EntityFrameworkCore;

namespace DropTablesSocial.Data;

public class PostRepo : IPostRepo {
    private readonly DropTablesContext _context;
    public PostRepo(DropTablesContext dropTablesContext) {
        _context = dropTablesContext;
    }
    public async Task<IEnumerable<Post>> GetAllPosts() {
        return await _context.Posts
            .Include(p => p.Likes).ToListAsync();
    }
    public async Task<Post> GetPostById(int id) {
        return await _context.Posts
            .Include(p => p.Likes).FirstOrDefaultAsync(p => p.PostId == id);
    }
    public async Task AddPost(Post post) {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }
    public async Task UpdatePost(Post post) {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }
    public async Task DeletePost(Post post) {
        _context.Remove(post);
        await _context.SaveChangesAsync();
    }
}