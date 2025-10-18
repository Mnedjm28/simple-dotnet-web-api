using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Category;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Application.Dtos;

namespace SimpleDotNetWebApiApp.Application.Handelers.Category
{
    public class UpdateCategoryHandler(IWriteCategoryRepo _categoryRepo, IMapper _mapper) : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Entities.Category>(request);       

            return _mapper.Map<CategoryDto>(await _categoryRepo.UpdateCategory(category));
        }
    }
}
