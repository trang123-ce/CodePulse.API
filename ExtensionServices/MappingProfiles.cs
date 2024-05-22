using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Models.DTO.Request;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.ExtensionServices
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        { 
            CreateMap<BlogPost, BlogPostDto>();
            CreateMap<Payment, CreatePayment>();
            CreateMap<RegisterRequestDto, IdentityUser>()
                .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email));
        }
    }
}
