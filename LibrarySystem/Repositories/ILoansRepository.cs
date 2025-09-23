using LibrarySystem.Data;
using LibrarySystem.Data.Entities;

namespace LibrarySystem.Repositories;

public interface ILoansRepository : IRepository<LoansEntity>;

public class LoansRepository(LibraryContext context) : BaseRepository<LoansEntity>(context), ILoansRepository;