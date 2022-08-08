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
    public class CategoriesController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(UniversityDBContext context, ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategorias()
        {
            _logger.LogWarning($"{nameof(CategoriesController)} - {nameof(GetCategorias)} Warning Level Log");
            _logger.LogError($"{nameof(CategoriesController)} - {nameof(GetCategorias)} Error Level Log");
            _logger.LogCritical($"{nameof(CategoriesController)} - {nameof(GetCategorias)} Critical Level Log"); 

            if (_context.Categorias == null)
          {
              return NotFound();
          }
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)

        {
            _logger.LogWarning($"{nameof(CategoriesController)} - {nameof(GetCategory)} Warning Level Log");
            _logger.LogError($"{nameof(CategoriesController)} - {nameof(GetCategory)} Error Level Log");
            _logger.LogCritical($"{nameof(CategoriesController)} - {nameof(GetCategory)} Critical Level Log");

            if (_context.Categorias == null)
          {
              return NotFound();
          }
            var category = await _context.Categorias.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            _logger.LogWarning($"{nameof(CategoriesController)} - {nameof(PutCategory)} Warning Level Log");
            _logger.LogError($"{nameof(CategoriesController)} - {nameof(PutCategory)} Error Level Log");
            _logger.LogCritical($"{nameof(CategoriesController)} - {nameof(PutCategory)} Critical Level Log");

            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _logger.LogWarning($"{nameof(CategoriesController)} - {nameof(PostCategory)} Warning Level Log");
            _logger.LogError($"{nameof(CategoriesController)} - {nameof(PostCategory)} Error Level Log");
            _logger.LogCritical($"{nameof(CategoriesController)} - {nameof(PostCategory)} Critical Level Log");

            if (_context.Categorias == null)
          {
              return Problem("Entity set 'UniversityDBContext.Categorias'  is null.");
          }
            _context.Categorias.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogWarning($"{nameof(CategoriesController)} - {nameof(DeleteCategory)} Warning Level Log");
            _logger.LogError($"{nameof(CategoriesController)} - {nameof(DeleteCategory)} Error Level Log");
            _logger.LogCritical($"{nameof(CategoriesController)} - {nameof(DeleteCategory)} Critical Level Log");

            if (_context.Categorias == null)
            {
                return NotFound();
            }
            var category = await _context.Categorias.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categorias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
