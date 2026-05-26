using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDockerPiaget.Data;
using ApiDockerPiaget.Models;
using ApiDockerPiaget.DTOs;

namespace ApiDockerPiaget.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfessoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProfessoresController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfessorDto>>> GetProfessores()
    {
        var professores = await _context.Professores.ToListAsync();
        return Ok(professores.Select(p => new ProfessorDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Email = p.Email,
            Disciplina = p.Disciplina,
            Titulacao = p.Titulacao,
            EscolaId = p.EscolaId
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfessorDto>> GetProfessor(int id)
    {
        var professor = await _context.Professores.FindAsync(id);
        if (professor == null) return NotFound();

        return Ok(new ProfessorDto
        {
            Id = professor.Id,
            Nome = professor.Nome,
            Email = professor.Email,
            Disciplina = professor.Disciplina,
            Titulacao = professor.Titulacao,
            EscolaId = professor.EscolaId
        });
    }

    [HttpPost]
    public async Task<ActionResult<ProfessorDto>> PostProfessor(ProfessorDto professorDto)
    {
        var professor = new Professor
        {
            Nome = professorDto.Nome,
            Email = professorDto.Email,
            Disciplina = professorDto.Disciplina,
            Titulacao = professorDto.Titulacao,
            EscolaId = professorDto.EscolaId
        };

        _context.Professores.Add(professor);
        await _context.SaveChangesAsync();

        professorDto.Id = professor.Id;
        return CreatedAtAction(nameof(GetProfessor), new { id = professor.Id }, professorDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProfessor(int id, ProfessorDto professorDto)
    {
        if (id != professorDto.Id) return BadRequest();

        var professor = await _context.Professores.FindAsync(id);
        if (professor == null) return NotFound();

        professor.Nome = professorDto.Nome;
        professor.Email = professorDto.Email;
        professor.Disciplina = professorDto.Disciplina;
        professor.Titulacao = professorDto.Titulacao;
        professor.EscolaId = professorDto.EscolaId;

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
//    public class ProfessoresController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public ProfessoresController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Professores
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessores()
//        {
//            return await _context.Professores
//                .Include(p => p.Escola)
//                .ToListAsync();
//        }

//        // GET: api/Professores/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Professor>> GetProfessor(int id)
//        {
//            var professor = await _context.Professores
//                .Include(p => p.Escola)
//                .FirstOrDefaultAsync(p => p.Id == id);

//            if (professor == null)
//                return NotFound();

//            return professor;
//        }

//        // POST: api/Professores
//        [HttpPost]
//        public async Task<ActionResult<Professor>> PostProfessor(Professor professor)
//        {
//            _context.Professores.Add(professor);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetProfessor), new { id = professor.Id }, professor);
//        }

//        // PUT: api/Professores/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutProfessor(int id, Professor professor)
//        {
//            if (id != professor.Id)
//                return BadRequest();

//            _context.Entry(professor).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProfessorExists(id))
//                    return NotFound();
//                else
//                    throw;
//            }

//            return NoContent();
//        }

//        // DELETE: api/Professores/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteProfessor(int id)
//        {
//            var professor = await _context.Professores.FindAsync(id);
//            if (professor == null)
//                return NotFound();

//            _context.Professores.Remove(professor);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ProfessorExists(int id)
//        {
//            return _context.Professores.Any(e => e.Id == id);
//        }
//    }
//}
