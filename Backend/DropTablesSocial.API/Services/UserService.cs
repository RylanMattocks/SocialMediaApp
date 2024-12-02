using AutoMapper;
using DropTablesSocial.Data;
using DropTablesSocial.Models;

namespace DropTablesSocial.API;

public class UserService : IUserService {
    private readonly IUserRepo _userRepo;
    private readonly IPostRepo _postRepo;
    private readonly IMapper _mapper;

    public UserService(IUserRepo userRepo, IMapper mapper, IPostRepo postRepo) {
        _userRepo = userRepo;
        _mapper = mapper;
        _postRepo = postRepo;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers() {
        var users = await _userRepo.GetAllUsers();
        var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
        return userDTOs;
    }

    public async Task<UserDTO> GetUserById(int id) {
        User user = await _userRepo.GetUserById(id) ?? throw new NullReferenceException("No user found");
        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;
    }

    public async Task AddUser(AddUserDTO addUserDTO) {
        if (await _userRepo.GetUserByUsername(addUserDTO.Username) is not null) throw new InvalidOperationException("User already exists");
        User user = _mapper.Map<User>(addUserDTO);
        await _userRepo.AddUser(user);
    }

    public async Task UpdateUser(int id, UpdateUserDTO updateUserDTO) {
        User user = await _userRepo.GetUserById(id) ?? throw new NullReferenceException("User not found");
        _mapper.Map(updateUserDTO, user);
        await _userRepo.UpdateUser(user);
    }

    public async Task DeleteUser(int id) {
        User user = await _userRepo.GetUserById(id) ?? throw new NullReferenceException("User not found");
        await _userRepo.DeleteUser(user);
    }

    public async Task LikePost(int userId, int postId) {
        Post post = await _postRepo.GetPostById(postId);
        User currentUser = await _userRepo.GetUserById(userId);
        if (!currentUser.Likes.Contains(post)) currentUser.Likes.Add(post);
        await _userRepo.UpdateUser(currentUser);
    }

    public async Task UnLikePost(int userId, int postId) {
        Post post = await _postRepo.GetPostById(postId);
        User currentUser = await _userRepo.GetUserById(userId);
        currentUser.Likes.Remove(post);
        await _userRepo.UpdateUser(currentUser);
    }

    public async Task FollowUser(int followerId, int followeeId) {
        User follower = await _userRepo.GetUserById(followerId);
        User followee = await _userRepo.GetUserById(followeeId);

        if (follower is null || followee is null) throw new NullReferenceException("Users not found");

        if (follower.Following.Any(f => f.UserId == followeeId)) throw new InvalidOperationException("Already following user");

        follower.Following.Add(followee);
        await _userRepo.UpdateUser(follower);
    }

    public async Task UnFollowUser(int followerId, int followeeId) {
        User follower = await _userRepo.GetUserById(followerId);
        User followee = await _userRepo.GetUserById(followeeId);

        if (follower is null || followee is null) throw new NullReferenceException("Users not found");

        if (!follower.Following.Any(f => f.UserId == followeeId)) throw new InvalidOperationException("Not following user");

        follower.Following.Remove(followee);
        await _userRepo.UpdateUser(follower);
    }

    public async Task<UserDTO> GetUserByUsername(string Username) {
        User user = await _userRepo.GetUserByUsername(Username) ?? throw new NullReferenceException("No user found");
        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;
    }
}