using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos.Item;

namespace SimpleDotNetWebApiApp.Application.Queries.Item
{
    public record GetItemsByCategorQuery : IRequest<List<ItemDto>>;
}
