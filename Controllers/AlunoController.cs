using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDockerPiaget.Data;
using ApiDockerPiaget.DTOs;
using AutoMapper;
using FluentValidation;
using ApiDockerPiaget.Models;

namespace ApiDockerPiaget.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<AlunoDto> _validator;

    public AlunosController(AppDbContext context, IMapper mapper, IValidator<AlunoDto> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAlunos()
    {
        var alunos = await _context.Alunos.Include(a => a.Escola).ToListAsync();
        return Ok(_mapper.Map<List<AlunoDto>>(alunos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDto>> GetAluno(int id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound();

        return Ok(_mapper.Map<AlunoDto>(aluno));
    }

    [HttpPost]
    public async Task<ActionResult<AlunoDto>> PostAluno(AlunoDto alunoDto)
    {
        var validationResult = await _validator.ValidateAsync(alunoDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var aluno = _mapper.Map<Aluno>(alunoDto);

        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();

        var alunoRetorno = _mapper.Map<AlunoDto>(aluno);
        return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, alunoRetorno);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAluno(int id, AlunoDto alunoDto)
    {
        if (id != alunoDto.Id) return BadRequest();

        var validationResult = await _validator.ValidateAsync(alunoDto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound();

        _mapper.Map(alunoDto, aluno);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAluno(int id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound();

        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

