using System.Linq.Expressions;
using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Repositories;

namespace LibrarySystem.Services;

public interface ILoansService : ICrudService<ILoansRepository, LoansEntity, LoansDto, CreateLoanRequestDto>
{

}

public class LoansService(ILoansRepository loansRepository, IBooksRepository booksRepository, IMembersRepository membersRepository, IDbUnitOfWork dbUnitOfWork, IMapper mapper) : 
    CrudService<ILoansRepository, LoansEntity, LoansDto, CreateLoanRequestDto>(loansRepository, dbUnitOfWork, mapper), ILoansService 
{
    public override async Task<List<LoansDto>> GetAllAsync(CancellationToken ct = default)
    {
        var includes = new List<Expression<Func<LoansEntity, object?>>>
        {
            loan => loan.Book,
            loan => loan.Member,
        };
        
        var entities = await loansRepository.FindAsync(_ => true, includes, false, ct);
        
        return mapper.Map<List<LoansDto>>(entities);
    }

    public override async Task<LoansDto> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var includes = new List<Expression<Func<LoansEntity, object?>>>
        {
            loan => loan.Book,
            loan => loan.Member,
        };
        
        var entity = await loansRepository.FindOneAsync(loan => loan.Key == id, includes, false, ct)
                     ?? throw new KeyNotFoundException();
        
        return mapper.Map<LoansDto>(entity);
    }
    
    public override async Task<LoansDto> CreateAsync(CreateLoanRequestDto request, CancellationToken ct = default)
    {
        var loanEntity = mapper.Map<LoansEntity>(request);
        
        var bookEntity = await booksRepository.GetByIdAsync(request.BookId, false, ct) ?? throw new KeyNotFoundException();
        var memberEntity = await membersRepository.GetByIdAsync(request.MemberId, false, ct) ?? throw new KeyNotFoundException();

        loanEntity.BookId = bookEntity.Id;
        loanEntity.MemberId = memberEntity.Id;
            
        // TODO: add a check if the book is already token or not 
        
        await loansRepository.AddAsync(loanEntity, ct);
        await dbUnitOfWork.SaveChangesAsync(ct);
        
        return mapper.Map<LoansDto>(loanEntity);
    }
}