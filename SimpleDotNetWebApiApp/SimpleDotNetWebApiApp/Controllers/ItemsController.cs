using Microsoft.AspNetCore.Mvc;
using SimpleDotNetWebApiApp.Data.Models;
using SimpleDotNetWebApiApp.Data;
using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Dtos;

namespace SimpleDotNetWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        readonly ApplicationDbContext _dbContext;
        readonly ILogger<ItemsController> _logger;

        public ItemsController(ApplicationDbContext dbContext, ILogger<ItemsController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Get() => await _dbContext.Set<Item>().ToListAsync();

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var record = await _dbContext.Set<Item>().FirstOrDefaultAsync(p => p.Id == id);

            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Create([FromForm] ItemDto item)
        {
            var record = new Item
            {
                Name = item.Name,
                Price = item.Price,
                CategoryId = item.CategoryId,
                Note = item.Note,
            };

            if (item.Image != null)
            {
                using var stream = new MemoryStream();

                await item.Image.CopyToAsync(stream);
                record.Image = stream.ToArray();
            }

            await _dbContext.Set<Item>().AddAsync(record);
            await _dbContext.SaveChangesAsync();

            return Ok(record);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] ItemDto item)
        {
            var record = await _dbContext.Set<Item>().FirstOrDefaultAsync(p => p.Id == item.Id);

            if (record == null)
            {
                _logger.LogDebug("Record not found #{id}", item.Id);
                return NotFound();
            }

            if (item.Image != null)
            {
                using var stream = new MemoryStream();

                await item.Image.CopyToAsync(stream);
                record.Image = stream.ToArray();
            }

            record.Name = item.Name;
            record.Price = item.Price;
            record.CategoryId = item.CategoryId;
            record.Note = item.Note;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _dbContext.Set<Item>().FirstAsync(p => p.Id == id);

            if (record == null)
                return NotFound();

            _dbContext.Set<Item>().Remove(record);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
