using AutoMapper;
using MediatR;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Application.Queries.Item;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;

namespace SimpleDotNetWebApiApp.Application.Handelers.Item
{
    public class GetItemsByCategoryHandler(IItemRepo _itemRepo, IMapper _mapper) : IRequestHandler<GetItemsByCategoryQuery, List<ItemDto>>
    {
        public async Task<List<ItemDto>> Handle(GetItemsByCategoryQuery request, CancellationToken cancellationToken)
         => _mapper.Map<List<ItemDto>>(await _itemRepo.GetItemsByCategory(request.categoryId));
    }
}
