namespace SocialMedia.Infrastructure.Mappings
{
    using AutoMapper;
    using SocialMedia.Core.DTOs;
    using SocialMedia.Core.Entities;

    public class AutomapperProfile :  Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
            CreateMap<SecurityDto, Security>().ReverseMap();            
        }
    }
}
