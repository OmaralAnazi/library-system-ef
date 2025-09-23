using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public interface IMemberService : ICrudService<IMembersRepository, MembersEntity, MembersDto, CreateMemberRequestDto>
{

}

public class MemberService(IMembersRepository membersRepository, IDbUnitOfWork dbUnitOfWork, IMapper mapper) : 
    CrudService<IMembersRepository, MembersEntity, MembersDto, CreateMemberRequestDto>(membersRepository, dbUnitOfWork, mapper), IMemberService 
{
    
}