﻿using System;
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
    public class NivelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NivelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Nivels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nivel>>> GetNivel()
        {
            return await _context.Nivel.ToListAsync();
        }

        // GET: api/Nivels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nivel>> GetNivel(int id)
        {
            var nivel = await _context.Nivel.FindAsync(id);

            if (nivel == null)
            {
                return NotFound();
            }

            return nivel;
        }

        // PUT: api/Nivels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNivel(int id, Nivel nivel)
        {
            if (id != nivel.NivelId)
            {
                return BadRequest();
            }

            _context.Entry(nivel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelExists(id))
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

        // POST: api/Nivels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nivel>> PostNivel(Nivel nivel)
        {
            _context.Nivel.Add(nivel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNivel", new { id = nivel.NivelId }, nivel);
        }

        // DELETE: api/Nivels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNivel(int id)
        {
            var nivel = await _context.Nivel.FindAsync(id);
            if (nivel == null)
            {
                return NotFound();
            }

            _context.Nivel.Remove(nivel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NivelExists(int id)
        {
            return _context.Nivel.Any(e => e.NivelId == id);
        }
    }
}
