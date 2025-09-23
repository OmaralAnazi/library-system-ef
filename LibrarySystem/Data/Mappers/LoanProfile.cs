using AutoMapper;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;

namespace LibrarySystem.Mappers;

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        // Entity -> DTO (BooksDto & MembersDto are mapped via their own profiles)
        CreateMap<LoansEntity, LoansDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Key));

        // Create DTO -> Entity
        // BookId/MemberId in DTO are *string keys*, but entity FK is long → set in service after lookup
        CreateMap<CreateLoanRequestDto, LoansEntity>()
            .ForMember(d => d.BookId,    o => o.Ignore())
            .ForMember(d => d.MemberId,  o => o.Ignore())
            .ForMember(d => d.LoanDate,  o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.Id,        o => o.Ignore())
            .ForMember(d => d.Key,       o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.DeletedAt, o => o.Ignore());
    }
}