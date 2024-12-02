using DropTablesSocial.Models;
using Microsoft.EntityFrameworkCore;

namespace DropTablesSocial.Data;

public class UserRepo : IUserRepo {
    private readonly DropTablesContext _context;

    public UserRepo(DropTablesContext dropTablesContext) {
        _context = dropTablesContext;
    }

    public async Task<IEnumerable<User>> GetAllUsers() {
        return await _context.Users
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .Include(u => u.Likes)
            .ToListAsync();
    }

    public async Task<User> GetUserById(int id) {
        return await _context.Users
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .Include(u => u.Likes)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task AddUser(User user) {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user) {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user) {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByUsername(string Username) {
        return await _context.Users
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .Include(u => u.Likes)
            .FirstOrDefaultAsync(u => u.Username == Username);
    }
}