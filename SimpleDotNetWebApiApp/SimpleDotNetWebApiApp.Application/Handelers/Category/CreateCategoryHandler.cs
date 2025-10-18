using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Category;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Handelers.Category
{
    public class CreateCategoryHandler(IWriteCategoryRepo _categoryRepo, IMapper _mapper) : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Entities.Category>(request);

            return _mapper.Map<CategoryDto>(await _categoryRepo.CreateCategory(category));
        }
    }
}
