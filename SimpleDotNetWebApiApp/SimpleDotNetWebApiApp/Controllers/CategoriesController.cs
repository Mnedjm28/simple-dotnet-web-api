using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleDotNetWebApiApp.Application.Commands.Category;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Application.Queries.Category;

namespace SimpleDotNetWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator, IMapper mapper, ILogger<ItemsController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get() => await mediator.Send(new GetCategoriesQuery());

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            return Ok(await mediator.Send(new GetCategoryQuery(id)));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromForm] CreateCategoryCommand item)
        {
            return Ok(await mediator.Send(item));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] UpdateCategoryCommand item)
        {
            var record = await mediator.Send(new UpdateCategoryCommand(item.Id, item.Name, item.Note));

            return record == null ? NotFound() : Ok(record);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteCategoryCommand(id));
            return Ok();
        }
    }
}
