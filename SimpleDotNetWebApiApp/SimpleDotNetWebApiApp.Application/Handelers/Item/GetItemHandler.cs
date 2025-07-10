using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Application.Queries.Item;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Item
{
    public class GetItemHandler(IItemRepo _itemRepo, IMapper _mapper) : IRequestHandler<GetItemQuery, ItemDto>
    {
        public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
         => _mapper.Map<ItemDto>(await _itemRepo.GetItem(request.id));
    }
}
