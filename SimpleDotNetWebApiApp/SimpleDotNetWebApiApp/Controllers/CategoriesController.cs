using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Data;

namespace SimpleDotNetWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(AppDbContext dbContext, ILogger<CategoriesController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get() => await dbContext.Set<Category>().ToListAsync();

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var record = await dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == id);

            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            var record = await dbContext.Set<Category>().AddAsync(category);

            await dbContext.SaveChangesAsync();

            return Ok(record.Entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Category category)
        {
            var record = await dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == category.Id);

            if (record == null)
            {
                logger.LogDebug("Record not found #{id}", category.Id);
                return NotFound();
            }

            record.Name = category.Name;
            record.Note = category.Note;

            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] JsonPatchDocument<Category> category)
        {
            var record = await dbContext.Set<Category>().FirstOrDefaultAsync(p => p.Id == id);

            if (record == null)
            {
                logger.LogDebug("Record not found #{id}", id);
                return NotFound();
            }

            category.ApplyTo(record);

            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await dbContext.Set<Category>().FirstAsync(p => p.Id == id);

            if (record == null)
                return NotFound();

            dbContext.Set<Category>().Remove(record);

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
