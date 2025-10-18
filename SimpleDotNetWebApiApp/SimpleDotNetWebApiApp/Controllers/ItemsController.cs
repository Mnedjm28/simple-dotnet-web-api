using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IItemRepo itemRepo, IMapper mapper, ILogger<ItemsController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get() => mapper.Map<List<ItemDto>>(await itemRepo.GetAllAsync());

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemDto>> Get(int id) => mapper.Map<ItemDto>(await itemRepo.GetByIdAsync(id));

        [HttpGet]
        [Route("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetByCategory(int categoryId) => mapper.Map<List<ItemDto>>(await itemRepo.GetItemsByCategoryAsync(categoryId));

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Create([FromForm] ItemDto item)
        {
            var record = mapper.Map<Item>(item);

            if (item.Image != null)
            {
                using var stream = new MemoryStream();

                await item.Image.CopyToAsync(stream);
                record.Image = stream.ToArray();
            }

            return Ok(mapper.Map<ItemDto>(await itemRepo.CreateAsync(record)));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] UpdateItemDto item)
        {
            var record = mapper.Map<Item>(item);

            if (item.Image != null)
            {
                using var stream = new MemoryStream();

                await item.Image.CopyToAsync(stream);
                record.Image = stream.ToArray();
            }

            return Ok(mapper.Map<ItemDto>(await itemRepo.UpdateAsync(record, item.IgnoreImage)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await itemRepo.DeleteAsync(id);
            return Ok();
        }
    }
}
