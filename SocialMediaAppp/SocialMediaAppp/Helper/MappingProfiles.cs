using AutoMapper;
using SocialMediaAppp.Dto;
using SocialMediaAppp.Models;

namespace SocialMediaAppp.Helper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Comment, CommentDto>();
			CreateMap<CommentDto, Comment>();
			CreateMap<Post, PostDto>();
			CreateMap<PostDto, Post>();
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
		}
	}
}