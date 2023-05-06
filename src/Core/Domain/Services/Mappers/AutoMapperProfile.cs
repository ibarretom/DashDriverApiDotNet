using AutoMapper;
using Core.Domain.Entities;
using Shared.ValueObject.DTO;
using Shared.ValueObject.Response;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoreTest")]
namespace Core.Domain.Services.Mappers;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoURL, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        CreateMap<User, UserResponse>();
    }
}
