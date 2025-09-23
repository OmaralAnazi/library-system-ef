using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Repositories;

namespace LibrarySystem.Services;

public interface IBooksService : ICrudService<IBooksRepository, BooksEntity, BooksDto, CreateBookRequestDto>
{

}

public class BooksService(IBooksRepository booksRepository, IDbUnitOfWork dbUnitOfWork, IMapper mapper) : 
    CrudService<IBooksRepository, BooksEntity, BooksDto, CreateBookRequestDto>(booksRepository, dbUnitOfWork, mapper), IBooksService 
{
    
}