using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos.Item;

namespace SimpleDotNetWebApiApp.Application.Queries.Item
{
    public record GetItemsByCategoryQuery(int categoryId) : IRequest<List<ItemDto>>;
}
