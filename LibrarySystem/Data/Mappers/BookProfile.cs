using AutoMapper;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Dtos;

namespace LibrarySystem.Data.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        // Entity -> Response DTO
        CreateMap<BooksEntity, BooksDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Key));
        
        // Create DTO -> Entity
        CreateMap<CreateBookRequestDto, BooksEntity>()
            .ForMember(d => d.Id,        o => o.Ignore())   // DB-generated or identity
            .ForMember(d => d.Key,       o => o.Ignore())   // set in service
            .ForMember(d => d.CreatedAt, o => o.Ignore())   // set in SaveChanges/Interceptor
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.DeletedAt, o => o.Ignore());
    }
}