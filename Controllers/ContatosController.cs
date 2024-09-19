using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExemploAPI.Data;
using ExemploAPI.Models;

namespace ExemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContatosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contatos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContato()
        {
            return await _context.Contato.ToListAsync();
        }

        // GET: api/Contatos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _context.Contato.FindAsync(id);

            if (contato == null)
            {
                return NotFound();
            }

            return contato;
        }

        // PUT: api/Contatos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(int id, Contato contato)
        {
            if (id != contato.ContatoId)
            {
                return BadRequest();
            }

            _context.Entry(contato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContatoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contatos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contato>> PostContato(Contato contato)
        {
            _context.Contato.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContato", new { id = contato.ContatoId }, contato);
        }

        // DELETE: api/Contatos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var contato = await _context.Contato.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }

            _context.Contato.Remove(contato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContatoExists(int id)
        {
            return _context.Contato.Any(e => e.ContatoId == id);
        }
    }
}
