using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Queries.Item
{
    public record GetItemsByCategorQuery : IRequest<List<ItemDto>>;
}
