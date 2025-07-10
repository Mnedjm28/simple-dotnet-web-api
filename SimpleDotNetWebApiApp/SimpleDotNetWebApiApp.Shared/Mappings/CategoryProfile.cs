using AutoMapper;
using SimpleDotNetWebApiApp.Application.Commands.Category;
using SimpleDotNetWebApiApp.Application.Dtos;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Shared.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(d => d.Name,
                            s => s.MapFrom(a => a.Name))
                .ForMember(d => d.Note,
                            s => s.MapFrom(a => a.Note));

            CreateMap<UpdateCategoryCommand, Category>()
                .ForMember(d => d.Name,
                            s => s.MapFrom(a => a.Name))
                .ForMember(d => d.Note,
                            s => s.MapFrom(a => a.Note));
        }
    }
}
