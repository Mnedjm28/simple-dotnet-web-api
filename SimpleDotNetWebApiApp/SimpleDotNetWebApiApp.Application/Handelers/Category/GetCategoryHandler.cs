using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Application.Queries.Category;

namespace SimpleDotNetWebApiApp.Application.Handelers.Category
{
    public class GetCategoryHandler(IReadCategoryRepo _categoryRepo, IMapper _mapper) : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
         => _mapper.Map<CategoryDto>(await _categoryRepo.GetCategory(request.id));
    }
}
