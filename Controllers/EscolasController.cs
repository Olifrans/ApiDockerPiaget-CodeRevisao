using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDockerPiaget.Data;
using ApiDockerPiaget.Models;
using ApiDockerPiaget.DTOs;
using AutoMapper;
using FluentValidation;

namespace ApiDockerPiaget.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EscolasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<EscolaDto> _validator;

    public EscolasController(AppDbContext context, IMapper mapper, IValidator<EscolaDto> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    // GET: api/Escolas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EscolaDto>>> GetEscolas()
    {
        var escolas = await _context.Escolas
            .Include(e => e.Alunos)
            .Include(e => e.Professores)
            .ToListAsync();

        var escolasDto = _mapper.Map<List<EscolaDto>>(escolas);
        return Ok(escolasDto);
    }

    // GET: api/Escolas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EscolaDto>> GetEscola(int id)
    {
        var escola = await _context.Escolas
            .Include(e => e.Alunos)
            .Include(e => e.Professores)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (escola == null)
            return NotFound();

        return Ok(_mapper.Map<EscolaDto>(escola));
    }

    // POST: api/Escolas
    [HttpPost]
    public async Task<ActionResult<EscolaDto>> PostEscola(EscolaDto escolaDto)
    {
        var validationResult = await _validator.ValidateAsync(escolaDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var escola = _mapper.Map<Escola>(escolaDto);

        _context.Escolas.Add(escola);
        await _context.SaveChangesAsync();

        var escolaRetorno = _mapper.Map<EscolaDto>(escola);
        return CreatedAtAction(nameof(GetEscola), new { id = escola.Id }, escolaRetorno);
    }

    // PUT: api/Escolas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEscola(int id, EscolaDto escolaDto)
    {
        if (id != escolaDto.Id) return BadRequest();

        var validationResult = await _validator.ValidateAsync(escolaDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var escola = await _context.Escolas.FindAsync(id);
        if (escola == null) return NotFound();

        _mapper.Map(escolaDto, escola);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Escolas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEscola(int id)
    {
        var escola = await _context.Escolas.FindAsync(id);
        if (escola == null) return NotFound();

        _context.Escolas.Remove(escola);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

