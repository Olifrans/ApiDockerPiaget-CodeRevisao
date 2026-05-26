using Microsoft.AspNetCore.Mvc;
using ApiDockerPiaget.Data;
using ApiDockerPiaget.DTOs;
using AutoMapper;
using FluentValidation;
using ApiDockerPiaget.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiDockerPiaget.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfessoresController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<ProfessorDto> _validator;

    public ProfessoresController(AppDbContext context, IMapper mapper, IValidator<ProfessorDto> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfessorDto>>> GetProfessores()
    {
        var professores = await _context.Professores.ToListAsync();
        return Ok(_mapper.Map<List<ProfessorDto>>(professores));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfessorDto>> GetProfessor(int id)
    {
        var professor = await _context.Professores.FindAsync(id);
        if (professor == null) return NotFound();

        return Ok(_mapper.Map<ProfessorDto>(professor));
    }

    [HttpPost]
    public async Task<ActionResult<ProfessorDto>> PostProfessor(ProfessorDto professorDto)
    {
        var validationResult = await _validator.ValidateAsync(professorDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var professor = _mapper.Map<Professor>(professorDto);

        _context.Professores.Add(professor);
        await _context.SaveChangesAsync();

        var professorRetorno = _mapper.Map<ProfessorDto>(professor);
        return CreatedAtAction(nameof(GetProfessor), new { id = professor.Id }, professorRetorno);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProfessor(int id, ProfessorDto professorDto)
    {
        if (id != professorDto.Id) return BadRequest();

        var validationResult = await _validator.ValidateAsync(professorDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var professor = await _context.Professores.FindAsync(id);
        if (professor == null) return NotFound();

        _mapper.Map(professorDto, professor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessor(int id)
    {
        var professor = await _context.Professores.FindAsync(id);
        if (professor == null) return NotFound();

        _context.Professores.Remove(professor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

