using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University1.DataAccess;
using University1.Models.DataModels;

namespace University1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ILogger<ChaptersController> _logger;   

        public ChaptersController(UniversityDBContext context, ILogger<ChaptersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chapters>>> GetChapters()
        {
            _logger.LogWarning($"{nameof(ChaptersController)} - {nameof(GetChapters)} Warning Level Log");
            _logger.LogError($"{nameof(ChaptersController)} - {nameof(GetChapters)} Error Level Log");
            _logger.LogCritical($"{nameof(ChaptersController)} - {nameof(GetChapters)} Critical Level Log");
            if (_context.Chapters == null)
          {
              return NotFound();
          }
            return await _context.Chapters.ToListAsync();
        }

        // GET: api/Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chapters>> GetChapter(int id)
        {
            _logger.LogWarning($"{nameof(ChaptersController)} - {nameof(GetChapter)} Warning Level Log");
            _logger.LogError($"{nameof(ChaptersController)} - {nameof(GetChapter)} Error Level Log");
            _logger.LogCritical($"{nameof(ChaptersController)} - {nameof(GetChapter)} Critical Level Log");
            if (_context.Chapters == null)
          {
              return NotFound();
          }
            var chapters = await _context.Chapters.FindAsync(id);

            if (chapters == null)
            {
                return NotFound();
            }

            return chapters;
        }

        // PUT: api/Chapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutChapters(int id, Chapters chapters)
        {
            _logger.LogWarning($"{nameof(ChaptersController)} - {nameof(PutChapters)} Warning Level Log");
            _logger.LogError($"{nameof(ChaptersController)} - {nameof(PutChapters)} Error Level Log");
            _logger.LogCritical($"{nameof(ChaptersController)} - {nameof(PutChapters)} Critical Level Log");
            if (id != chapters.Id)
            {
                return BadRequest();
            }

            _context.Entry(chapters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChaptersExists(id))
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

        // POST: api/Chapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Chapters>> PostChapters(Chapters chapters)
        {
            _logger.LogWarning($"{nameof(ChaptersController)} - {nameof(PostChapters)} Warning Level Log");
            _logger.LogError($"{nameof(ChaptersController)} - {nameof(PostChapters)} Error Level Log");
            _logger.LogCritical($"{nameof(ChaptersController)} - {nameof(PostChapters)} Critical Level Log");
            if (_context.Chapters == null)
          {
              return Problem("Entity set 'UniversityDBContext.Chapters'  is null.");
          }
            _context.Chapters.Add(chapters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChapters", new { id = chapters.Id }, chapters);
        }

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteChapters(int id)
        {
            _logger.LogWarning($"{nameof(ChaptersController)} - {nameof(DeleteChapters)} Warning Level Log");
            _logger.LogError($"{nameof(ChaptersController)} - {nameof(DeleteChapters)} Error Level Log");
            _logger.LogCritical($"{nameof(ChaptersController)} - {nameof(DeleteChapters)} Critical Level Log");
            if (_context.Chapters == null)
            {
                return NotFound();
            }
            var chapters = await _context.Chapters.FindAsync(id);
            if (chapters == null)
            {
                return NotFound();
            }

            _context.Chapters.Remove(chapters);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChaptersExists(int id)
        {
            return (_context.Chapters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
