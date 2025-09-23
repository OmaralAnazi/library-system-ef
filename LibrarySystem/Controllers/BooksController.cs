using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBooksService service)
    : CrudController<IBooksService, IBooksRepository, BooksEntity, BooksDto, CreateBookRequestDto>(service)
{

}