using SimpleDotNetWebApiApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Modularized services
builder.Services.AddConfigurationSettings(builder.Configuration);

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddFilterConfiguration();

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddAuthorizationPolicies();

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Pipeline
app.UseDevelopmentTools();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCustomMiddlewares();
app.MapControllers();

app.ApplyDatabaseMigrations();

app.Run();
