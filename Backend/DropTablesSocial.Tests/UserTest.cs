using AutoMapper;
using DropTablesSocial.Data;
using DropTablesSocial.API;
using Moq;
using DropTablesSocial.Models;

namespace DropTablesSocial.Tests;

public class UserTest
{
    private readonly Mock<IUserRepo> _mockUserRepo;
    private readonly Mock<IPostRepo> _mockPostRepo;
    private readonly IMapper _mapper;
    private readonly UserService _userService;
    private readonly PostService _postService;
    public UserTest() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
        _mockUserRepo = new Mock<IUserRepo>();
        _mockPostRepo = new Mock<IPostRepo>();
        _postService = new PostService(_mockPostRepo.Object, _mapper);
        _userService = new UserService(_mockUserRepo.Object, _mapper, _mockPostRepo.Object);
    }
    [Fact]
    public async void TestGetAllUsers()
    {
        List<User> userList = [];
        for (int i = 0; i < 5; i++) {
            userList.Add(new User {UserId = i, Username = $"user{i}"});
        }

        _mockUserRepo.Setup(repo => repo.GetAllUsers()).ReturnsAsync(userList);
        var result = await _userService.GetAllUsers();

        Assert.Equal(userList.Count, result.Count());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async void TestGetUserById(int id)
    {
        List<User> userList = [];
        for (int i = 0; i < 5; i++) {
            userList.Add(new User {UserId = i, Username = $"user{i}"});
        }

        _mockUserRepo.Setup(repo => repo.GetUserById(It.IsAny<int>())).ReturnsAsync(userList.FirstOrDefault(u => u.UserId == id));

        var result = await _userService.GetUserById(id);

        Assert.Equal(id, result.UserId);
    }

    [Fact]
    public async void TestGetUserByIdThrowsException()
    {
        List<User> userList = [];
        for (int i = 0; i < 5; i++) {
            userList.Add(new User {UserId = i, Username = $"user{i}"});
        }

        _mockUserRepo.Setup(repo => repo.GetUserById(It.IsAny<int>())).ReturnsAsync(userList.FirstOrDefault(u => u.UserId == 10));

        await Assert.ThrowsAnyAsync<NullReferenceException>(() => _userService.GetUserById(10));
    }

    [Fact]
    public async void TestAddUser()
    {
        List<User> userList = [];
        for (int i = 0; i < 5; i++) {
            userList.Add(new User {UserId = i, Username = $"user{i}"});
        }

        AddUserDTO addUserDTO = new() {Username = "new"};
        User user = new() {UserId = 5, Username = ""};

        _mockUserRepo.Setup(repo => repo.AddUser(It.IsAny<User>())).Callback(() => userList.Add(user));

        await _userService.AddUser(addUserDTO);

        Assert.Contains(userList, u => u.UserId == user.UserId);
    }

    [Fact]
    public async void TestAddUserThrowsException()
    {
        List<User> userList = [];
        for (int i = 0; i < 5; i++) {
            userList.Add(new User {UserId = i, Username = $"user{i}"});
        }

        AddUserDTO addUserDTO = new() {Username = "user4"};
        User user = new() {UserId = 5, Username = "user4"};

        _mockUserRepo.Setup(repo => repo.GetUserByUsername(It.IsAny<string>())).ReturnsAsync(userList.FirstOrDefault(u => u.Username == addUserDTO.Username));
        _mockUserRepo.Setup(repo => repo.AddUser(It.IsAny<User>())).Callback(() => userList.Add(user));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.AddUser(addUserDTO));

        Assert.Equal("User already exists", exception.Message);

        _mockUserRepo.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void TestUpdateUser()
    {
        var existingUser = new User { UserId = 1, Username = "oldUsername", Email = "oldemail@example.com" };
        var updateUserDTO = new UpdateUserDTO { Username = "newUsername", Email = "newemail@example.com" };

        _mockUserRepo.Setup(repo => repo.GetUserById(1)).ReturnsAsync(existingUser);
        _mockUserRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        _userService.UpdateUser(1, updateUserDTO);

        _mockUserRepo.Verify(repo => repo.GetUserById(1), Times.Once);
        _mockUserRepo.Verify(repo => repo.UpdateUser(It.Is<User>(u => u.Username == "newUsername" && u.Email == "newemail@example.com")), Times.Once);

    }

    [Fact]
    public async Task TestUpdateUserThrowsException()
    {
        int userId = 1;
        var updateUserDTO = new UpdateUserDTO { Username = "newUsername", Email = "newemail@example.com" };

        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User)null);

        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.UpdateUser(userId, updateUserDTO));
        Assert.Equal("User not found", exception.Message);
    }

    [Fact]
    public void TestDeleteUser()
    {
        int userId = 1;
        var existingUser = new User { UserId = userId, Username = "testUser", Email = "testuser@example.com" };

        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(existingUser);
        _mockUserRepo.Setup(repo => repo.DeleteUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        _userService.DeleteUser(userId);

        _mockUserRepo.Verify(repo => repo.GetUserById(userId), Times.Once);
        _mockUserRepo.Verify(repo => repo.DeleteUser(It.Is<User>(u => u.UserId == userId)), Times.Once);

    }

    [Fact]
    public async Task TestDeleteUserThrowsException()
    {

        int userId = 1;

        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User)null);

        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.DeleteUser(userId));
        Assert.Equal("User not found", exception.Message);
    }

    [Fact]
    public void TestLikePost()
    {
        int userId = 1;
        int postId = 1;
        var post = new Post { PostId = postId, Content = "Sample Post" };
        var user = new User { UserId = userId, Likes = new List<Post>() };

        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(post);
        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);
        _mockUserRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        _userService.LikePost(userId, postId);

        Assert.Contains(post, user.Likes);
        _mockUserRepo.Verify(repo => repo.UpdateUser(user), Times.Once);

    }

    [Fact(Skip = "fixing")]
    public async Task TestLikePostThrowsException()
    {

        int userId = 1;
        int postId = 1;

        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User)null);
        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(new Post());

        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.LikePost(userId, postId));
        Assert.Equal("User not found", exception.Message);

        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(new User());
        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync((Post)null);

        exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.LikePost(userId, postId));
        Assert.Equal("Post not found", exception.Message);
    }

    [Fact(Skip = "fixing")]
    public async Task TestLikePostAlreadyLikedPost()
    {
        int userId = 1;
        int postId = 1;
        var post = new Post { PostId = postId, Content = "Sample Post" };
        var user = new User { UserId = userId, Likes = new List<Post> { post } };

        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(post);
        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);
        _mockUserRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        await _userService.LikePost(userId, postId);

        Assert.Single(user.Likes);
        _mockUserRepo.Verify(repo => repo.UpdateUser(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void TestUnlikePost()
    {
        int userId = 1;
        int postId = 1;
        var post = new Post { PostId = postId, Content = "Sample Post" };
        var user = new User { UserId = userId, Likes = new List<Post> { post } };

        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(post);
        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);
        _mockUserRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        _userService.UnLikePost(userId, postId);

        Assert.DoesNotContain(post, user.Likes);
        _mockUserRepo.Verify(repo => repo.UpdateUser(user), Times.Once);

    }

    [Fact(Skip = "fixing")]
    public async Task TestUnLikePostThrowsException()
    {
        int userId = 1;
        int postId = 1;

        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User)null);
        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(new Post());

        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.UnLikePost(userId, postId));
        Assert.Equal("User not found", exception.Message);

        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(new User());
        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync((Post)null);

        exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.UnLikePost(userId, postId));
        Assert.Equal("Post not found", exception.Message);
    }

    [Fact(Skip = "fixing")]
    public async Task TestUnLikePostUserAlreadyUnlike()
    {
        int userId = 1;
        int postId = 1;
        var post = new Post { PostId = postId, Content = "Sample Post" };
        var user = new User { UserId = userId, Likes = new List<Post>() };

        _mockPostRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(post);
        _mockUserRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);
        _mockUserRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        await _userService.UnLikePost(userId, postId);

        Assert.Empty(user.Likes);
        _mockUserRepo.Verify(repo => repo.UpdateUser(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public void TestFollowUser()
    {
        int followerId = 1;
        int followeeId = 2;
        var follower = new User { UserId = followerId, Following = new List<User>() };
        var followee = new User { UserId = followeeId, Username = "Followee" };

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync(follower);
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync(followee);
        _mockUserRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        _userService.FollowUser(followerId, followeeId);

        Assert.Contains(followee, follower.Following);
        _mockUserRepo.Verify(repo => repo.UpdateUser(follower), Times.Once);

    }

    [Fact]
    public async Task TestFollowUserThrowsExceptionNoUser()
    {
        int followerId = 1;
        int followeeId = 2;

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync((User)null);
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync(new User());

        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.FollowUser(followerId, followeeId));
        Assert.Equal("Users not found", exception.Message);

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync(new User());
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync((User)null);

        exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.FollowUser(followerId, followeeId));
        Assert.Equal("Users not found", exception.Message);
    }

    [Fact]
    public async Task TestFollowUserThrowsExceptionFollowingAlready()
    {
        int followerId = 1;
        int followeeId = 2;
        var followee = new User { UserId = followeeId, Username = "Followee" };
        var follower = new User
        {
            UserId = followerId,
            Following = new List<User> { followee }
        };

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync(follower);
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync(followee);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.FollowUser(followerId, followeeId));
        Assert.Equal("Already following user", exception.Message);
    }

    [Fact]
    public void TestUnfollowUser()
    {
        int followerId = 1;
        int followeeId = 2;
        var followee = new User { UserId = followeeId, Username = "Followee" };
        var follower = new User
        {
            UserId = followerId,
            Following = new List<User> { followee }
        };

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync(follower);
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync(followee);
        _mockUserRepo.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(Task.CompletedTask);

        _userService.UnFollowUser(followerId, followeeId);

        Assert.DoesNotContain(followee, follower.Following);
        _mockUserRepo.Verify(repo => repo.UpdateUser(follower), Times.Once);
    }

    [Fact]
    public async Task UnFollowUser_UserNotFound_ThrowsException()
    {
        int followerId = 1;
        int followeeId = 2;

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync((User)null);
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync(new User());

        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.UnFollowUser(followerId, followeeId));
        Assert.Equal("Users not found", exception.Message);

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync(new User());
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync((User)null);

        exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.UnFollowUser(followerId, followeeId));
        Assert.Equal("Users not found", exception.Message);
    }

    [Fact]
    public async Task UnFollowUser_NotFollowingUser_ThrowsException()
    {
        int followerId = 1;
        int followeeId = 2;
        var followee = new User { UserId = followeeId, Username = "Followee" };
        var follower = new User
        {
            UserId = followerId,
            Following = new List<User>()
        };

        _mockUserRepo.Setup(repo => repo.GetUserById(followerId)).ReturnsAsync(follower);
        _mockUserRepo.Setup(repo => repo.GetUserById(followeeId)).ReturnsAsync(followee);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.UnFollowUser(followerId, followeeId));
        Assert.Equal("Not following user", exception.Message);
    }

    [Fact]
    public async void TestGetUserByUsername()
    {
        string username = "user1";
        var user = new User { UserId = 1, Username = username, Email = "user1@example.com" };
        _mockUserRepo.Setup(repo => repo.GetUserByUsername(username)).ReturnsAsync(user);

        var result = await _userService.GetUserByUsername(username);

        Assert.NotNull(result);
        Assert.Equal(user.UserId, result.UserId);
        Assert.Equal(user.Username, result.Username);
        Assert.Equal(user.Email, result.Email);
        _mockUserRepo.Verify(repo => repo.GetUserByUsername(username), Times.Once);
    }

    [Fact]
    public async Task GetUserByUsername_UserNotFound_ThrowsException()
    {
        string username = "nonexistentUser";
        _mockUserRepo.Setup(repo => repo.GetUserByUsername(username)).ReturnsAsync((User)null);

        var exception = await Assert.ThrowsAsync<NullReferenceException>(() => _userService.GetUserByUsername(username));
        Assert.Equal("No user found", exception.Message);

        _mockUserRepo.Verify(repo => repo.GetUserByUsername(username), Times.Once);
    }
}