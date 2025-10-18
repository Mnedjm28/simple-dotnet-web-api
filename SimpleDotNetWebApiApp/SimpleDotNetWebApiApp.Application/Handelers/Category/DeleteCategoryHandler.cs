using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Category;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Category
{
    public class DeleteCategoryHandler(IWriteCategoryRepo _categoryRepo) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepo.DeleteCategory(request.id);
        }
    }
}
