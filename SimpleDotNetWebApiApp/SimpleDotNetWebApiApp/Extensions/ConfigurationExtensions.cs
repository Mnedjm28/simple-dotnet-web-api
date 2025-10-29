namespace SimpleDotNetWebApiApp.Extensions
{
    public static class ConfigurationExtensions
    {
        public static WebApplicationBuilder AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            // Load custom configuration files in order
            builder.Configuration
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder;
        }

        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration config)
        {
            /*
             * Method 1: Recommended and Modern Way (using Options pattern)
             * This automatically binds the config sections to POCO classes 
             * and makes them available through IOptions<T>.
            */
            services.Configure<AttachmentOptions>(config.GetSection("Attachments"));
            services.Configure<JwtOptions>(config.GetSection("Jwt"));

            /*
             * Method 2: Register configuration instances directly as singletons.
             * Use this if you prefer direct access via DI (without IOptions<T>)
            */
            //services.AddSingleton(config.GetSection("Attachments").Get<AttachmentOptions>());

            /*
             * Method 3: Manual binding (explicit object creation)
             * Good if you want to customize or preprocess data before registration.
            */
            //var attachmentOptions = new AttachmentOptions();
            //config.GetSection("Attachments").Bind(attachmentOptions);
            //services.AddSingleton(attachmentOptions);

            return services;
        }
    }
}
