using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Application.Queries.Category;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Category
{
    public class GetCategoriesHandler(IReadCategoryRepo _categoryRepo, IMapper _mapper) : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
         => _mapper.Map<List<CategoryDto>>(await _categoryRepo.GetCategories());
    }
}
