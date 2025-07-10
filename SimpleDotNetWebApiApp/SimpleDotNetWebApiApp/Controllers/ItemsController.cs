using Microsoft.AspNetCore.Mvc;
using MediatR;
using SimpleDotNetWebApiApp.Application.Queries.Item;
using SimpleDotNetWebApiApp.Application.Commands.Item;
using AutoMapper;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IMediator mediator, IMapper mapper, ILogger<ItemsController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get() => await mediator.Send(new GetItemsByCategorQuery());

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemDto>> Get(int id)
        {
            return Ok(await mediator.Send(new GetItemQuery(id)));
        }

        [HttpGet]
        [Route("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetByCategory(int categoryId) => await mediator.Send(new GetItemsByCategoryQuery(categoryId));

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Create([FromForm] CreateItemCommand item)
        {
            return Ok(await mediator.Send(new CreateItemCommand(item.Name, item.Price, item.Note, item.Image, item.CategoryId)));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] UpdateItemCommand item)
        {
            var record = await mediator.Send(new UpdateItemCommand(item.Id, item.Name, item.Price, item.Note, item.Image, item.CategoryId, item.IgnoreImage));

            return record == null ? NotFound() : Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteItemCommand(id));
            return Ok();
        }
    }
}
