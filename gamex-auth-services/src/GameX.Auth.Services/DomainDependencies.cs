namespace gamex.Auth.Services;

public static class DomainDependencies
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();

        // USED POSTGRES DATABASE
        services.AddDbContext<UserDbContext>((sp, option) => {
            var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>();

            option.AddInterceptors(auditableInterceptor);
            option.UseNpgsql(configuration["ConnectionStrings:Default"])
            .UseSnakeCaseNamingConvention();
            
        });
        
        // Redis Cache Service
        services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddSingleton<IImageService, ImageService>();
        services.AddSingleton<IUserActivityCacheService, UserActivityCacheService>();
        services.AddSingleton<IUserAgentParserService, UserAgentParserService>();
        services.AddSingleton<IFileOperationService, FileOperationService>();

        // Services
        services.AddScoped<ILoginTokenService, LoginTokenService>();
        services.AddScoped<ICodeGenerationService, CodeGenerationService>();
        services.AddScoped<IUserNameGenerator, UserNameGenerator>();

        services.AddScoped<IUserLogActivityRepository, UserLogActivityRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    
}