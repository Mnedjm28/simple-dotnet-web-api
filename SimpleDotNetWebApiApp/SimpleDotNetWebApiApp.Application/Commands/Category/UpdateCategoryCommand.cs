using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Commands.Category
{
    public record UpdateCategoryCommand(int Id, string? Name, string? Note) : IRequest<CategoryDto>;
}
