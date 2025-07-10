using MediatR;

namespace SimpleDotNetWebApiApp.Application.Commands.Category
{
    public record DeleteCategoryCommand(int id) : IRequest;
}
