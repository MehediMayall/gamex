using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

try
{
    // App Settings
    builder.Services.ConfigureCacheAppSettings(builder.Configuration); 

    // Postgres Database
    builder.Services.AddPostgres<UserDbContext>(builder.Configuration);


    // Cross Cutting Dependencies
    builder.Services.AddProjectDependencies(builder.Configuration);

    // Domain Dependencies
    builder.Services.AddDomainDependencies(builder.Configuration);

    // Health Check
    builder.Services.AddHealthChecks();

    // Global Exception Logger
    builder.Services.AddSingleton<GlobalExceptionsLogger>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddDataProtection();

    // CORS
    builder.Services.AddCors(options => 
    options.AddPolicy("AllowAll",
        policy  =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        })
    );



    // Application Insights Using Opentelemetry
    builder.Services.AddOpenTelemetry()
        .WithMetrics(option => 
            option.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("gamex.Auth.Services"))
                .AddPrometheusExporter()
                .AddAspNetCoreInstrumentation()
                .AddProcessInstrumentation()
                .AddRuntimeInstrumentation()       
        );

    



    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {

        app.MapOpenApi();
        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    } else {
        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/api/auth/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }

    // Authorization
    app.UseAuthorization();

    // Use Cors
    app.UseCors("AllowAll");    

    // Global Exception Logger
    app.UseMiddleware<GlobalExceptionsLogger>();


    // Use Compression
    app.UseResponseCompression();

    // HTTPS
    app.UseHttpsRedirection();


    // Authentication Endpoints
    app.AddAuthEndpoints();

    // Common Endpoints
    app.AddCommonEndpoints(builder.Configuration);


    // Health Check
    app.MapHealthChecks("/health");

    // Prometheus Endpoing
    app.MapPrometheusScrapingEndpoint();
 
     
    // REGISTER STATIC RESOURCE
    builder.RegisterStaticResource(app, builder.Configuration);

    // Startup Message    
    StartupMessage.SetStartupMessage(builder.Configuration);
    app.Run();

}
catch (Exception ex)
{
    Console.WriteLine($"gamex Auth has been terminated. Reason:" + ex.GetAllExceptions());
    Log.Fatal(ex.GetAllExceptions());    
}
finally{
    Log.CloseAndFlush();
}


public partial class Program {}