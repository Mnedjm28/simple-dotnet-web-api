using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleDotNetWebApiApp;
using SimpleDotNetWebApiApp.Authorization;
using SimpleDotNetWebApiApp.Filters;
using SimpleDotNetWebApiApp.Middlewares;
using System.Text;
using MediatR;
using SimpleDotNetWebApiApp.Infrastructure.Data;
using SimpleDotNetWebApiApp.Infrastructure.Contracts;
using SimpleDotNetWebApiApp.Infrastructure.Repositories;
using SimpleDotNetWebApiApp.Application.Validation.Item;
using SimpleDotNetWebApiApp.Application.Handelers.Item;
using FluentValidation;
using SimpleDotNetWebApiApp.Shared.Mappings;
using SimpleDotNetWebApiApp.Shared;
using SimpleDotNetWebApiApp.Application.Behaviors;
using SimpleDotNetWebApiApp.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region Configuration

builder.Configuration.AddJsonFile("config.json");

/*
 * Read Configuration Medthod 1
*/
builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

/*
 * Read Configuration Medthod 2
*/
//builder.Services.AddSingleton(builder.Configuration.GetSection("Attachments").Get<AttachmentOptions>());

/*
 * Read Configuration Medthod 3
*/
//var attachmentOptions = new AttachmentOptions();
//builder.Configuration.GetSection("Attachments").Bind(attachmentOptions);
//builder.Services.AddSingleton(attachmentOptions);

#endregion Configuration

#region DataBase

// Configure DataBase + How to call connection string configuration from appsettings.json
builder.Services.AddDbContext<AppDbContext>(cng => cng.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion DataBase

#region Filters

// Configure Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActivityFilter>();
    options.Filters.Add<PermissionBasedAuthorizationFilter>();
});

#endregion Filters

#region Authentication

// JWT Bearer Authentication Shceme
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer, // for multi issuers use ValidIssuers
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience, // for multi audiences use ValidAudiences
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
    };
});

// Basic Authentication Shceme

//builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

#endregion Authentication

#region Authorization

// Policy Based Authorization
builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorizationHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminsOnly", policy =>
    {
        policy.RequireRole(Constants.ADMIN);
    });

    options.AddPolicy("UsersOnly", policy =>
    {
        policy.RequireClaim("Role", Constants.USER);
    });

    options.AddPolicy("AgeGreaterThan25", policy => policy.AddRequirements(new AgeGreaterThan25Requirement()));

    options.AddPolicy("GuestsOnly", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return context.User.IsInRole(Constants.GUEST);
        });
    });
});

#endregion Authorization

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<IItemRepo, ItemRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateItemHandler).Assembly);
});
builder.Services.AddAutoMapper(typeof(ItemProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CreateItemCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<ProfilingMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();

app.MapControllers();

app.Run();
