using AutoMapper;
using SimpleDotNetWebApiApp.Application.Commands.Item;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Shared.Mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>().ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<CreateItemCommand, Item>()
                .ForMember(d => d.Name,
                            s => s.MapFrom(a => a.Name))
                .ForMember(d => d.CategoryId,
                            s => s.MapFrom(a => a.CategoryId))
                .ForMember(d => d.Note,
                            s => s.MapFrom(a => a.Note))
                .ForMember(d => d.Price,
                            s => s.MapFrom(a => a.Price))
                .ForMember(d => d.Image, s => s.Ignore());

            CreateMap<UpdateItemCommand, Item>()
                .ForMember(d => d.Name,
                            s => s.MapFrom(a => a.Name))
                .ForMember(d => d.CategoryId,
                            s => s.MapFrom(a => a.CategoryId))
                .ForMember(d => d.Note,
                            s => s.MapFrom(a => a.Note))
                .ForMember(d => d.Price,
                            s => s.MapFrom(a => a.Price))
                .ForMember(d => d.Image, s => s.Ignore());
        }
    }
}
