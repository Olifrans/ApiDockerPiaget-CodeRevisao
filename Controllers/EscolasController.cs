using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDockerPiaget.Data;
using ApiDockerPiaget.Models;
using ApiDockerPiaget.DTOs;

namespace ApiDockerPiaget.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EscolasController : ControllerBase
{
    private readonly AppDbContext _context;

    public EscolasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Escolas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EscolaDto>>> GetEscolas()
    {
        var escolas = await _context.Escolas
            .Include(e => e.Alunos)
            .Include(e => e.Professores)
            .ToListAsync();

        var escolasDto = escolas.Select(e => new EscolaDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Endereco = e.Endereco,
            Cidade = e.Cidade,
            Telefone = e.Telefone,
            Alunos = e.Alunos.Select(MapToAlunoDto).ToList(),
            Professores = e.Professores.Select(MapToProfessorDto).ToList()
        }).ToList();

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

        if (escola == null) return NotFound();

        var escolaDto = new EscolaDto
        {
            Id = escola.Id,
            Nome = escola.Nome,
            Endereco = escola.Endereco,
            Cidade = escola.Cidade,
            Telefone = escola.Telefone,
            Alunos = escola.Alunos.Select(MapToAlunoDto).ToList(),
            Professores = escola.Professores.Select(MapToProfessorDto).ToList()
        };

        return Ok(escolaDto);
    }

    // POST: api/Escolas
    [HttpPost]
    public async Task<ActionResult<EscolaDto>> PostEscola(Escola escola)
    {
        _context.Escolas.Add(escola);
        await _context.SaveChangesAsync();

        var escolaDto = new EscolaDto
        {
            Id = escola.Id,
            Nome = escola.Nome,
            Endereco = escola.Endereco,
            Cidade = escola.Cidade,
            Telefone = escola.Telefone
        };

        return CreatedAtAction(nameof(GetEscola), new { id = escola.Id }, escolaDto);
    }

    // Métodos de mapeamento auxiliares
    private static AlunoDto MapToAlunoDto(Aluno a) => new()
    {
        Id = a.Id,
        Nome = a.Nome,
        Email = a.Email,
        DataNascimento = a.DataNascimento,
        Serie = a.Serie,
        EscolaId = a.EscolaId
    };

    private static ProfessorDto MapToProfessorDto(Professor p) => new()
    {
        Id = p.Id,
        Nome = p.Nome,
        Email = p.Email,
        Disciplina = p.Disciplina,
        Titulacao = p.Titulacao,
        EscolaId = p.EscolaId
    };
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
//    public class EscolasController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public EscolasController(AppDbContext context) => _context = context;

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Escola>>> GetEscolas()
//            => await _context.Escolas
//                .Include(e => e.Alunos)
//                .Include(e => e.Professores)
//                .ToListAsync();





//        //[HttpGet("{id}")]
//        //public async Task<ActionResult<Escola>> GetEscola(int id)
//        //{
//        //    var escola = await _context.Escolas
//        //        .Include(e => e.Alunos)
//        //        .Include(e => e.Professores)
//        //        .FirstOrDefaultAsync(e => e.Id == id);

//        //    return escola ?? NotFound();
//        //}





//        //[HttpGet("{id}")]
//        //public async Task<ActionResult<Escola>> GetEscola(int id)
//        //{
//        //    var escola = await _context.Escolas
//        //        .Include(e => e.Alunos)
//        //        .Include(e => e.Professores)
//        //        .FirstOrDefaultAsync(e => e.Id == id);

//        //    if (escola == null)
//        //        return NotFound();

//        //    return escola;
//        //}


//        [HttpGet("{id}")]
//        public async Task<ActionResult<Escola>> GetEscola(int id)
//        {
//            var escola = await _context.Escolas
//                .Include(e => e.Alunos)
//                .Include(e => e.Professores)
//                .FirstOrDefaultAsync(e => e.Id == id);

//            return escola is not null ? escola : NotFound();
//        }



//        [HttpPost]
//        public async Task<ActionResult<Escola>> PostEscola(Escola escola)
//        {
//            _context.Escolas.Add(escola);
//            await _context.SaveChangesAsync();
//            return CreatedAtAction(nameof(GetEscola), new { id = escola.Id }, escola);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEscola(int id, Escola escola)
//        {
//            if (id != escola.Id) return BadRequest();
//            _context.Entry(escola).State = EntityState.Modified;

//            try { await _context.SaveChangesAsync(); }
//            catch (DbUpdateConcurrencyException) { return NotFound(); }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEscola(int id)
//        {
//            var escola = await _context.Escolas.FindAsync(id);
//            if (escola == null) return NotFound();

//            _context.Escolas.Remove(escola);
//            await _context.SaveChangesAsync();
//            return NoContent();
//        }
//    }

//}
