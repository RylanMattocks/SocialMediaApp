using AutoMapper;
using DropTablesSocial.Data;
using DropTablesSocial.Models;

namespace DropTablesSocial.API;

public class PostService : IPostService {
    private readonly IPostRepo _postRepo;
    private readonly IMapper _mapper;
    public PostService(IPostRepo postRepo, IMapper mapper) {
        _mapper = mapper;
        _postRepo = postRepo;
    }
    public async Task<IEnumerable<PostDTO>> GetAllPosts() {
        var posts = await _postRepo.GetAllPosts();
        var postDTOs = _mapper.Map<IEnumerable<PostDTO>>(posts);
        return postDTOs;
    }
    public async Task<PostDTO> GetPostById(int id) {
        Post post = await _postRepo.GetPostById(id) ?? throw new NullReferenceException("No post found");
        PostDTO postDTO = _mapper.Map<PostDTO>(post);
        return postDTO;
    }
    public async Task AddPost(AddPostDTO addPostDTO) {
        Post post = _mapper.Map<Post>(addPostDTO);
        await _postRepo.AddPost(post);
    }
    public async Task UpdatePost(int id, UpdatePostDTO updatePostDTO) {
        Post post = await _postRepo.GetPostById(id);
        if(post is not null) {
            _mapper.Map(updatePostDTO, post);
            await _postRepo.UpdatePost(post);
        }
    }
    public async Task DeletePost(int id) {
        Post post = await _postRepo.GetPostById(id);
        await _postRepo.DeletePost(post);
    }
}