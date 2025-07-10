using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Queries.Category
{
    public record GetCategoryQuery(int id) : IRequest<CategoryDto>;
}
