using ApiDockerPiaget.Data;
using ApiDockerPiaget.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


namespace ApiDockerPiaget.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscolasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EscolasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Escola>>> GetEscolas()
            => await _context.Escolas
                .Include(e => e.Alunos)
                .Include(e => e.Professores)
                .ToListAsync();





        //[HttpGet("{id}")]
        //public async Task<ActionResult<Escola>> GetEscola(int id)
        //{
        //    var escola = await _context.Escolas
        //        .Include(e => e.Alunos)
        //        .Include(e => e.Professores)
        //        .FirstOrDefaultAsync(e => e.Id == id);

        //    return escola ?? NotFound();
        //}





        //[HttpGet("{id}")]
        //public async Task<ActionResult<Escola>> GetEscola(int id)
        //{
        //    var escola = await _context.Escolas
        //        .Include(e => e.Alunos)
        //        .Include(e => e.Professores)
        //        .FirstOrDefaultAsync(e => e.Id == id);

        //    if (escola == null)
        //        return NotFound();

        //    return escola;
        //}


        [HttpGet("{id}")]
        public async Task<ActionResult<Escola>> GetEscola(int id)
        {
            var escola = await _context.Escolas
                .Include(e => e.Alunos)
                .Include(e => e.Professores)
                .FirstOrDefaultAsync(e => e.Id == id);

            return escola is not null ? escola : NotFound();
        }



        [HttpPost]
        public async Task<ActionResult<Escola>> PostEscola(Escola escola)
        {
            _context.Escolas.Add(escola);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEscola), new { id = escola.Id }, escola);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEscola(int id, Escola escola)
        {
            if (id != escola.Id) return BadRequest();
            _context.Entry(escola).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { return NotFound(); }

            return NoContent();
        }

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

}
