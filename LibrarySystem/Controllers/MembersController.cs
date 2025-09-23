using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Repositories;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(IMemberService service)
    : CrudController<IMemberService, IMembersRepository, MembersEntity, MembersDto, CreateMemberRequestDto>(service)
{

}