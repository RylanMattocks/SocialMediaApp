using AutoMapper;
using DropTablesSocial.Models;

namespace DropTablesSocial.Data;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<User, AddUserDTO>().ReverseMap();
        CreateMap<UpdateUserDTO, User>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<User, UpdateUserDTO>();
        CreateMap<User, UserInfoDTO>().ReverseMap();
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Followers, opt => opt.MapFrom(src => src.Followers))
            .ForMember(dest => dest.Following, opt => opt.MapFrom(src => src.Following))
            .ReverseMap();

        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<Post, AddPostDTO>().ReverseMap();
        CreateMap<Post, UpdatePostDTO>().ReverseMap();
    }
}