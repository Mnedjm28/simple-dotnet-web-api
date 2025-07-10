using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Application.Queries.Item;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Item
{
    public class GetItemsHandler(IItemRepo _itemRepo, IMapper _mapper) : IRequestHandler<GetItemsByCategorQuery, List<ItemDto>>
    {
        public async Task<List<ItemDto>> Handle(GetItemsByCategorQuery request, CancellationToken cancellationToken)
         => _mapper.Map<List<ItemDto>>(await _itemRepo.GetItems());
    }
}
