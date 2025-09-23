using System.Linq.Expressions;
using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Exceptions;

namespace LibrarySystem.Services;

public interface ILoansService : ICrudService<ILoansRepository, LoansEntity, LoansDto, CreateLoanRequestDto>
{
    Task<LoansDto> ReturnLoanAsync(string loanId, CancellationToken ct = default);
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

        var entity = await loansRepository.FindOneAsync(loan => loan.Key == id, includes, false, ct) ??
                     throw ExceptionFactory.EntityNotFoundException();
        
        return mapper.Map<LoansDto>(entity);
    }
    
    public override async Task<LoansDto> CreateAsync(CreateLoanRequestDto request, CancellationToken ct = default)
    {
        var loanEntity = mapper.Map<LoansEntity>(request);

        var bookEntity = await booksRepository.GetByIdAsync(request.BookId, false, ct) ?? throw ExceptionFactory.EntityNotFoundException();
        var memberEntity = await membersRepository.GetByIdAsync(request.MemberId, false, ct) ?? throw ExceptionFactory.EntityNotFoundException();

        loanEntity.BookId = bookEntity.Id;
        loanEntity.MemberId = memberEntity.Id;
            
        if (!await IsBookAvailable(bookEntity, ct))
        {
            throw ExceptionFactory.NotAcceptableActionException();
        }
        
        await loansRepository.AddAsync(loanEntity, ct);
        await dbUnitOfWork.SaveChangesAsync(ct);
        
        // Workaround: set navs only for DTO mapping; in real projects reload with includes instead.
        loanEntity.Book = bookEntity;
        loanEntity.Member = memberEntity;
        
        return mapper.Map<LoansDto>(loanEntity);
    }

    public async Task<LoansDto> ReturnLoanAsync(string loanId, CancellationToken ct = default)
    {
        var includes = new List<Expression<Func<LoansEntity, object?>>>
        {
            loan => loan.Book,
            loan => loan.Member,
        };

        var loan = await loansRepository.FindOneAsync(loan => loan.Key == loanId, includes, true, ct)
                     ?? throw ExceptionFactory.EntityNotFoundException();

        if (loan.ReturnDate != null)
        {
            throw ExceptionFactory.NotAcceptableActionException();
        } 
        
        loan.ReturnDate = DateTime.UtcNow;
        await dbUnitOfWork.SaveChangesAsync(ct);
        
        return mapper.Map<LoansDto>(loan);
    }

    private async Task<bool> IsBookAvailable(BooksEntity book, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;

        var activeLoan = await loansRepository.FindOneAsync(
            l => l.BookId == book.Id && (l.ReturnDate == null || l.ReturnDate > now),
            cancellationToken: ct
        );

        return activeLoan is null;
    }
}