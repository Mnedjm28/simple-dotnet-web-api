using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Data;
using SimpleDotNetWebApiApp.Data.Models;

namespace SimpleDotNetWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ApplicationDbContext _dbContext;
        readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ApplicationDbContext dbContext, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get() => await _dbContext.Set<Category>().ToListAsync();

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var record = await _dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == id);

            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            var record = await _dbContext.Set<Category>().AddAsync(category);

            await _dbContext.SaveChangesAsync();

            return Ok(record.Entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Category category)
        {
            var record = await _dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == category.Id);

            if (record == null)
            {
                _logger.LogDebug("Record not found #{id}", category.Id);
                return NotFound();
            }

            record.Name = category.Name;
            record.Note = category.Note;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _dbContext.Set<Category>().FirstAsync(p => p.Id == id);

            if (record == null)
                return NotFound();

            _dbContext.Set<Category>().Remove(record);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
