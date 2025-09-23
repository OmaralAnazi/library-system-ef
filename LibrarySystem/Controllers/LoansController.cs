using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController(ILoansService service)
    : CrudController<ILoansService, ILoansRepository, LoansEntity, LoansDto, CreateLoanRequestDto>(service)
{
    [HttpPost("return/{id}")]
    public async Task<IActionResult> ReturnLoanAsync(string id, CancellationToken ct = default)
    {
        return Ok(await Service.ReturnLoanAsync(id, ct));
    }
}