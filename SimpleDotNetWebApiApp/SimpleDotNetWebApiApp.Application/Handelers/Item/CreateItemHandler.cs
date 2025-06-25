using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Item;
using SimpleDotNetWebApiApp.Application.Dtos.Item;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Item
{
    public class CreateItemHandler(IItemRepo _itemRepo, IMapper _mapper) : IRequestHandler<CreateItemCommand, CreateItemDto>
    {
        public async Task<CreateItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Entities.Item>(request.Item);

            if (request.Item.Image != null)
            {
                using var stream = new MemoryStream();

                await request.Item.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }

            return _mapper.Map<CreateItemDto>(await _itemRepo.CreateItem(item));

            //var item = _mapper.Map<Domain.Entities.Item>(request.Item);
            //var item = new Domain.Entities.Item
            //{
            //    Name = request.Name,
            //    Price = request.Price,
            //    CategoryId = request.CategoryId
            //};
        }
    }
}
