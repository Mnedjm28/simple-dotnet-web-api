using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos.Item;

namespace SimpleDotNetWebApiApp.Application.Queries.Item
{
    public record GetItemQuery(int id) : IRequest<ItemDto>;
}
