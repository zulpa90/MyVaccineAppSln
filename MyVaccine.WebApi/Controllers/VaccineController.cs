using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyVaccine.WebApi.Dtos;
using MyVaccine.WebApi.Literals;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VaccineController : ControllerBase
{
    private readonly IVaccineService _vaccineService;

    public VaccineController(IVaccineService vaccineService)
    {
        _vaccineService = vaccineService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VaccineRequestDto request)
    {
        var newVaccine = await _vaccineService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = newVaccine.VaccineId }, newVaccine);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vaccines = await _vaccineService.GetAllAsync();
        return Ok(vaccines);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vaccine = await _vaccineService.GetByIdAsync(id);
        if (vaccine == null)
        {
            return NotFound();
        }
        return Ok(vaccine);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] VaccineRequestDto request)
    {
        await _vaccineService.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _vaccineService.DeleteAsync(id);
        return NoContent();
    }
}