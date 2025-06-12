using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Authorization;
using SimpleDotNetWebApiApp.Data;
using SimpleDotNetWebApiApp.Filters;
using System.Security.Claims;

namespace SimpleDotNetWebApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        readonly ApplicationDbContext _dbContext;
        readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext dbContext, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [LogSensitiveAction]
        [CheckPermission(Permission.ReadProducts)]
        [Authorize(Roles = $"{Constants.ADMIN},{Constants.USER}")]        
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var user = User.Identity.Name;
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _dbContext.Set<Product>().ToListAsync();
        }

        [HttpGet]        
        [Route("{id}")]
        [Authorize(Policy = "UsersOnly")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var record = await _dbContext.Set<Product>().FirstOrDefaultAsync(p => p.Id == id);

            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            var record = await _dbContext.Set<Product>().AddAsync(product);

            await _dbContext.SaveChangesAsync();

            return Ok(record.Entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            var record = await _dbContext.Set<Product>().FirstOrDefaultAsync(p => p.Id == product.Id);

            if (record == null)
            {
                _logger.LogDebug("Record not found #{id}", product.Id);
                return NotFound();
            }

            record.Name = product.Name;
            record.Sku = product.Sku;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _dbContext.Set<Product>().FirstAsync(p => p.Id == id);

            if (record == null)
                return NotFound();

            _dbContext.Set<Product>().Remove(record);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
