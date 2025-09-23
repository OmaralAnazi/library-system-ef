using LibrarySystem.Data;
using LibrarySystem.Data.Entities;

namespace LibrarySystem.Data.Repositories;

public interface ILoansRepository : IRepository<LoansEntity>;

public class LoansRepository(LibraryContext context) : BaseRepository<LoansEntity>(context), ILoansRepository;