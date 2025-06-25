using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos.Item;

namespace SimpleDotNetWebApiApp.Application.Commands.Item
{
    //public record CreateItemCommand(string? Name, double Price, int CategoryId) : IRequest<CreateItemDto>;
    public record CreateItemCommand(CreateItemDto Item) : IRequest<CreateItemDto>;
}
