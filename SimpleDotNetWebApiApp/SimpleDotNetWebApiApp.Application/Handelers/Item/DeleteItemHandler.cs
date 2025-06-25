using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Item;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Item
{
    public class DeleteItemHandler(IItemRepo _itemRepo) : IRequestHandler<DeleteItemCommand>
    {
        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            await _itemRepo.DeleteItem(request.id);
        }
    }
}
