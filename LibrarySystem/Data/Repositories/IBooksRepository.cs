using LibrarySystem.Data;
using LibrarySystem.Data.Entities;

namespace LibrarySystem.Data.Repositories;

public interface IBooksRepository : IRepository<BooksEntity>;

public class BooksRepository(LibraryContext context) : BaseRepository<BooksEntity>(context), IBooksRepository;
