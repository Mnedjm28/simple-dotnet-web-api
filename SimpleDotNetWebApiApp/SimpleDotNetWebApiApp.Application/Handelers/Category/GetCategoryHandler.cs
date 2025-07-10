using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Application.Queries.Category;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Category
{
    public class GetCategoryHandler(ICategoryRepo _categoryRepo, IMapper _mapper) : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
         => _mapper.Map<CategoryDto>(await _categoryRepo.GetCategory(request.id));
    }
}
