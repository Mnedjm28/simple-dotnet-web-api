using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Category;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Category
{
    public class CreateCategoryHandler(ICategoryRepo _categoryRepo, IMapper _mapper) : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Entities.Category>(request);

            return _mapper.Map<CategoryDto>(await _categoryRepo.CreateCategory(category));
        }
    }
}
