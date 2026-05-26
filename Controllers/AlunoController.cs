using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDockerPiaget.Data;
using ApiDockerPiaget.Models;
using ApiDockerPiaget.DTOs;

namespace ApiDockerPiaget.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlunosController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAlunos()
    {
        var alunos = await _context.Alunos.Include(a => a.Escola).ToListAsync();
        return Ok(alunos.Select(a => new AlunoDto
        {
            Id = a.Id,
            Nome = a.Nome,
            Email = a.Email,
            DataNascimento = a.DataNascimento,
            Serie = a.Serie,
            EscolaId = a.EscolaId
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDto>> GetAluno(int id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound();

        return Ok(new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Email = aluno.Email,
            DataNascimento = aluno.DataNascimento,
            Serie = aluno.Serie,
            EscolaId = aluno.EscolaId
        });
    }

    [HttpPost]
    public async Task<ActionResult<AlunoDto>> PostAluno(AlunoDto alunoDto)
    {
        var aluno = new Aluno
        {
            Nome = alunoDto.Nome,
            Email = alunoDto.Email,
            DataNascimento = alunoDto.DataNascimento,
            Serie = alunoDto.Serie,
            EscolaId = alunoDto.EscolaId
        };

        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();

        alunoDto.Id = aluno.Id;
        return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, alunoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAluno(int id, AlunoDto alunoDto)
    {
        if (id != alunoDto.Id) return BadRequest();

        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null) return NotFound();

        aluno.Nome = alunoDto.Nome;
        aluno.Email = alunoDto.Email;
        aluno.DataNascimento = alunoDto.DataNascimento;
        aluno.Serie = alunoDto.Serie;
        aluno.EscolaId = alunoDto.EscolaId;

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





















//using ApiDockerPiaget.Data;
//using ApiDockerPiaget.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Text.Json.Serialization;


//namespace ApiDockerPiaget.Controllers
//{

//    [Route("api/[controller]")]
//    [ApiController]
//    public class AlunosController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public AlunosController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Alunos
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
//        {
//            return await _context.Alunos
//                .Include(a => a.Escola)
//                .ToListAsync();
//        }

//        // GET: api/Alunos/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Aluno>> GetAluno(int id)
//        {
//            var aluno = await _context.Alunos
//                .Include(a => a.Escola)
//                .FirstOrDefaultAsync(a => a.Id == id);

//            if (aluno == null)
//                return NotFound();

//            return aluno;
//        }

//        // POST: api/Alunos
//        [HttpPost]
//        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
//        {
//            _context.Alunos.Add(aluno);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
//        }

//        // PUT: api/Alunos/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
//        {
//            if (id != aluno.Id)
//                return BadRequest();

//            _context.Entry(aluno).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!AlunoExists(id))
//                    return NotFound();
//                else
//                    throw;
//            }

//            return NoContent();
//        }

//        // DELETE: api/Alunos/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteAluno(int id)
//        {
//            var aluno = await _context.Alunos.FindAsync(id);
//            if (aluno == null)
//                return NotFound();

//            _context.Alunos.Remove(aluno);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool AlunoExists(int id)
//        {
//            return _context.Alunos.Any(e => e.Id == id);
//        }
//    }


//}
