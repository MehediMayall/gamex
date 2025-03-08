using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;



namespace gamex.Auth.Services;

public static class CrossCuttingDependencies
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // SERILOG
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        // JSON Format Config
        services.AddControllers()
        .AddJsonOptions(options => 
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.WriteIndented = false;
        });

        // Response Compression Services
        services.AddResponseCompression(option => {
            option.EnableForHttps = true;
            option.Providers.Add<GzipCompressionProvider>();
            option.Providers.Add<BrotliCompressionProvider>();
        });

        // Configure compression options for Gzip and Brotli
        services.Configure<GzipCompressionProviderOptions>(option =>{
            option.Level = System.IO.Compression.CompressionLevel.Fastest;
        });

        services.Configure<BrotliCompressionProviderOptions>(option => {
            option.Level = System.IO.Compression.CompressionLevel.Fastest;
        });


        // Validators
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>));


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(),includeInternalTypes: true);
        services.AddScoped<IValidator<JWTSettings>, JWTTokenValidation>();


        // MediatR
        services.AddMediatR(cfg =>{
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(PerformancePipelineBehavior<,>));
        });

        // MassTransit
        services.AddMassTransitWithRabbitMQ(configuration);

        // Redis Cache
        services.AddRedis(configuration);


        // JWT
        services.RegisterJWT(configuration);

        // Swagger
        services.RegisterSwagger(configuration);


        // Background Job using Quartz
        services.AddQuartz(option =>
        {
            option.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = 1;
            });

            var jobKey = JobKey.Create(nameof(SaveUserLoginActivity));


            option.AddJob<SaveUserLoginActivity>(jobKey)
                .AddTrigger(trigger => 
                    trigger.ForJob(jobKey)
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(1)
                        .RepeatForever()));
        });
        services.AddQuartzHostedService();

        return services;
    }



    // JWT
    public static IServiceCollection RegisterJWT(this IServiceCollection services, IConfiguration configuration)
    {
        JWTSettings jwtSettings = configuration.GetSection("JWTSettings").Get<JWTSettings>();
        

        // JWT
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,                
            };
        });

        Log.Information("RegisterJWT: SUCCECSSFULL");

        return services;
    }

    // Swagger
    public static IServiceCollection RegisterSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        // API Versioning
        services.AddApiVersioning(options => {

            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;

        });
        
        var xmlFile = $"gamex.Auth.Services.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        Console.WriteLine(xmlPath);
        if (!File.Exists(xmlPath))
            Console.WriteLine($"NOT FOUND ----> {xmlPath}");


        // Add Swagger
        services.AddSwaggerGen(options => {
            options.SwaggerDoc( "v1", 
            new OpenApiInfo
            {
                Title = "gamex Auth Services API",
                Version = "v1",
                Description = "gamex Auth Services API",
                TermsOfService = new Uri("https://www.linkedin.com/in/mehedisun"),
                Contact = new OpenApiContact
                {
                    Name = "Mehedi",
                    Email = "mehedi.sun@example.com",
                    Url = new Uri("https://github.com/MehediMayall")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/MehediMayall/gamex.Auth.Services/blob/main/LICENSE")
                },

            });

            // XML Comment Documentation

            // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(xmlPath);

        });

        return services;
    }
    
}