using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Commands.Category
{
    public record CreateCategoryCommand(string? Name, string? Note) : IRequest<CategoryDto>;
}
