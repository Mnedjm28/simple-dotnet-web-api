using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using SimpleDotNetWebApiApp.Application.Behaviors;
using SimpleDotNetWebApiApp.Application.Handelers.Category;
using SimpleDotNetWebApiApp.Application.Validation.Item;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Infrastructure.Repositories;
using SimpleDotNetWebApiApp.Shared.Mappings;

namespace SimpleDotNetWebApiApp.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddFluentValidationAutoValidation();

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IItemRepo, ItemRepo>();
            services.AddScoped<IReadCategoryRepo, ReadCategoryRepo>();
            services.AddScoped<IWriteCategoryRepo, WriteCategoryRepo>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateCategoryHandler).Assembly);
            });
            services.AddAutoMapper(typeof(ItemProfile).Assembly);
            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
