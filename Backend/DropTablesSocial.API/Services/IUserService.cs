using DropTablesSocial.Models;

namespace DropTablesSocial.API;

public interface IUserService {
    public Task<IEnumerable<UserDTO>> GetAllUsers();
    public Task<UserDTO> GetUserById(int id);
    public Task AddUser(AddUserDTO user);
    public Task UpdateUser(int id, UpdateUserDTO user);
    public Task DeleteUser(int id);
    public Task LikePost(int userId, int postId);
    public Task UnLikePost(int userId, int postId);
    public Task FollowUser(int followerId, int followeeId);
    public Task UnFollowUser(int followerId, int followeeId);
    public Task<UserDTO> GetUserByUsername(string Username);
}