using AutoMapper;
using DropTablesSocial.Data;
using DropTablesSocial.API;
using Moq;
using DropTablesSocial.Models;

namespace DropTablesSocial.Tests;

public class PostTest
{
    private readonly Mock<IPostRepo> _mockRepo;
    private readonly IMapper _mapper;
    private readonly PostService _postService;
    public PostTest() {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
        _mockRepo = new Mock<IPostRepo>();
        _postService = new PostService(_mockRepo.Object, _mapper);
    }

    [Fact]
    public async void TestGetAllPosts()
    {
        List<Post> postList = [];
        for (int i = 0; i < 5; i++) {
            postList.Add(new Post {PostId = i, UserId = i, Content = "content"});
        }

        _mockRepo.Setup(repo => repo.GetAllPosts()).ReturnsAsync(postList);
        var result = await _postService.GetAllPosts();

        Assert.Equal(postList.Count, result.Count());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async void TestGetPostById(int id)
    {
        List<Post> postList = [];
        for (int i = 0; i < 5; i++) {
            postList.Add(new Post {PostId = i, UserId = i, Content = "content"});
        }

        _mockRepo.Setup(repo => repo.GetPostById(It.IsAny<int>())).ReturnsAsync(postList.FirstOrDefault(p => p.PostId == id));

        var result = await _postService.GetPostById(id);

        Assert.Equal(id, result.PostId);
    }

    [Fact]
    public async void TestGetPostByIdThrowsException()
    {
        List<Post> postList = [];
        for (int i = 0; i < 5; i++) {
            postList.Add(new Post {PostId = i, UserId = i, Content = "content"});
        }

        _mockRepo.Setup(repo => repo.GetPostById(It.IsAny<int>())).ReturnsAsync(postList.FirstOrDefault(p => p.PostId == 10));


        await Assert.ThrowsAnyAsync<NullReferenceException>(() => _postService.GetPostById(10));
    }

    [Fact]
    public async void TestAddPost()
    {
        List<Post> postList = [];
        for (int i = 0; i < 5; i++) {
            postList.Add(new Post {PostId = i, UserId = i, Content = "content"});
        }

        AddPostDTO addPostDTO = new() {UserId = 6, Content = "new content"};
        Post post = new() {PostId = 6, UserId = 6, Content = "new content"};

        _mockRepo.Setup(repo => repo.AddPost(It.IsAny<Post>())).Callback(() => postList.Add(post));

        await _postService.AddPost(addPostDTO);

        Assert.Contains(postList, p => p.PostId == post.PostId);
    }

    [Fact]
    public async Task TestUpdatePost()
    {
        // Arrange
        int postId = 1;
        var existingPost = new Post
        {
            PostId = postId,
            Content = "Old Content"
        };

        var updatePostDTO = new UpdatePostDTO
        {
            Content = "New Content"
        };

        _mockRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(existingPost);
        _mockRepo.Setup(repo => repo.UpdatePost(It.IsAny<Post>())).Returns(Task.CompletedTask);

        await _postService.UpdatePost(postId, updatePostDTO);

        _mockRepo.Verify(repo => repo.GetPostById(postId), Times.Once);
        _mockRepo.Verify(
            repo => repo.UpdatePost(It.Is<Post>(post =>
                post.PostId == existingPost.PostId &&
                post.Content == "New Content"
            )),
            Times.Once
        );
    }

    [Fact]
    public async Task TestUpdatePostWhenPostNotFound()
    {
        int postId = 1;
        var updatePostDTO = new UpdatePostDTO
        {
            Content = "New Content"
        };

        _mockRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync((Post)null);

        await _postService.UpdatePost(postId, updatePostDTO);

        _mockRepo.Verify(repo => repo.GetPostById(postId), Times.Once);
        _mockRepo.Verify(repo => repo.UpdatePost(It.IsAny<Post>()), Times.Never);
    }

    [Fact]
    public async Task TestDeletePost()
    {
        int postId = 1;
        var existingPost = new Post
        {
            PostId = postId,
            Content = "Test Content"
        };

        _mockRepo.Setup(repo => repo.GetPostById(postId)).ReturnsAsync(existingPost);
        _mockRepo.Setup(repo => repo.DeletePost(It.IsAny<Post>())).Returns(Task.CompletedTask);

        await _postService.DeletePost(postId);

        _mockRepo.Verify(repo => repo.GetPostById(postId), Times.Once);
        _mockRepo.Verify(repo => repo.DeletePost(It.Is<Post>(post =>
            post.PostId == existingPost.PostId
        )), Times.Once);
    }
}