using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public interface ILoansService : ICrudService<ILoansRepository, LoansEntity, LoansDto, CreateLoanRequestDto>
{

}

public class LoansService(ILoansRepository loansRepository, IDbUnitOfWork dbUnitOfWork, IMapper mapper) : 
    CrudService<ILoansRepository, LoansEntity, LoansDto, CreateLoanRequestDto>(loansRepository, dbUnitOfWork, mapper), ILoansService 
{
    
}