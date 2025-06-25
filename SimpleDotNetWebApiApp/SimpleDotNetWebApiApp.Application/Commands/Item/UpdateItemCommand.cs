using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos.Item;

namespace SimpleDotNetWebApiApp.Application.Commands.Item
{
    public record UpdateItemCommand(UpdateItemDto Item) : IRequest<UpdateItemDto>;
}
