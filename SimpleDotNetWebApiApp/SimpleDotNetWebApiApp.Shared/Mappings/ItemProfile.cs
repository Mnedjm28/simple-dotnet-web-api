using AutoMapper;
using SimpleDotNetWebApiApp.Application.Commands.Item;
using SimpleDotNetWebApiApp.Application.Dtos.Item;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Shared.Mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<CreateItemDto, Item>();
            CreateMap<Item, CreateItemDto>();
            CreateMap<CreateItemCommand, CreateItemDto>()
                .ForMember(x => x.Name,
                            m => m.MapFrom(a => a.Item.Name))
                .ForMember(x => x.CategoryId,
                            m => m.MapFrom(a => a.Item.CategoryId))
                .ForMember(x => x.Note,
                            m => m.MapFrom(a => a.Item.Note))
                .ForMember(x => x.Price,
                            m => m.MapFrom(a => a.Item.Price));
            CreateMap<UpdateItemDto, Item>();
            CreateMap<Item, UpdateItemDto>();
        }
    }
}
