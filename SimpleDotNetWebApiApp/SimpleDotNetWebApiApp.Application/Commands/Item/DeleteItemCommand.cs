using MediatR;

namespace SimpleDotNetWebApiApp.Application.Commands.Item
{
    public record DeleteItemCommand(int id) : IRequest;
}
