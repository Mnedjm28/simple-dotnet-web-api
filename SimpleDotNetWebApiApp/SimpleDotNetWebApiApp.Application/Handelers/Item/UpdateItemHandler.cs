using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Commands.Item;
using SimpleDotNetWebApiApp.Application.Dtos.Item;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Item
{
    public class UpdateItemHandler(IItemRepo _itemRepo, IMapper _mapper) : IRequestHandler<UpdateItemCommand, UpdateItemDto>
    {
        public async Task<UpdateItemDto> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Entities.Item>(request.Item);

            if (request.Item.Image != null)
            {
                using var stream = new MemoryStream();

                await request.Item.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }

            return _mapper.Map<UpdateItemDto>(await _itemRepo.UpdateItem(item));
        }
    }
}
