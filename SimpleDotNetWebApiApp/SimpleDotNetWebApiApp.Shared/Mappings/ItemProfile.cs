using AutoMapper;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Shared.Mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>().ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<ItemDto, Item>().ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Item, UpdateItemDto>().ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<UpdateItemDto, Item>().ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
