using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Queries.Category
{
    public record GetCategoriesQuery : IRequest<List<CategoryDto>>;
}
