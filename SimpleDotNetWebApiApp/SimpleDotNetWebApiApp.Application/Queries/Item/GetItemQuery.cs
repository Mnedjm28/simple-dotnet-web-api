using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Queries.Item
{
    public record GetItemQuery(int id) : IRequest<ItemDto>;
}
