using LibrarySystem.Data;
using LibrarySystem.Data.Entities;

namespace LibrarySystem.Repositories;

public interface IMembersRepository : IRepository<MembersEntity>;

public class MembersRepository(LibraryContext context) : BaseRepository<MembersEntity>(context), IMembersRepository;