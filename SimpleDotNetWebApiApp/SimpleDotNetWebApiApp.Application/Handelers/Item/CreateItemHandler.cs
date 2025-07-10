using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Item;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Item
{
    public class CreateItemHandler(IItemRepo _itemRepo, IMapper _mapper) : IRequestHandler<CreateItemCommand, ItemDto>
    {
        public async Task<ItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Entities.Item>(request);

            if (request.Image != null)
            {
                using var stream = new MemoryStream();

                await request.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }

            return _mapper.Map<ItemDto>(await _itemRepo.CreateItem(item));
        }
    }
}
