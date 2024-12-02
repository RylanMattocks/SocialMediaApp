using DropTablesSocial.Models;

namespace DropTablesSocial.Data;

public interface IUserRepo {
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User> GetUserById(int id);
    public Task AddUser(User user);
    public Task UpdateUser(User user);
    public Task DeleteUser(User user);
    public Task<User> GetUserByUsername(string Username);
}