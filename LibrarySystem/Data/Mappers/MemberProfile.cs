using AutoMapper;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;

namespace LibrarySystem.Data.Mappers;

public class MemberProfile : Profile
{
    public MemberProfile()
    {
        // Entity -> DTO
        CreateMap<MembersEntity, MembersDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Key));

        // Create DTO -> Entity (audit/key handled elsewhere)
        CreateMap<CreateMemberRequestDto, MembersEntity>()
            .ForMember(d => d.Id,        o => o.Ignore())
            .ForMember(d => d.Key,       o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.DeletedAt, o => o.Ignore());
    }
}