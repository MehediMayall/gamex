using System.Threading.RateLimiting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;


var builder = WebApplication.CreateBuilder(args);

try
{
    // Project Dependencies
    builder.Services.AddProjectDependencies(builder.Configuration);

    // Reverse Proxy
    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

    
    // CORS
    // builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()));
    builder.Services.AddCors(options => 
        options.AddPolicy("AllowAll",
            policy  =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            })
    );


    // Rate Limiting using Fixed Window. Limiting to 10 requests and block for 10 seconds
    builder.Services.AddRateLimiter(option =>{
        option.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

        option.AddPolicy("fixed-by-ip", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromSeconds(10)
            }));
    });


    // App Metrics: Process, Runtime, AspNetCore
    builder.Services.AddOpenTelemetry()
        .WithMetrics(option => 
            option.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("gamex.ApiGateway"))
                .AddPrometheusExporter()
                .AddAspNetCoreInstrumentation()
                .AddProcessInstrumentation()
                .AddRuntimeInstrumentation()       
        );



    // Health Check
    builder.Services.AddHealthChecks();

    builder.Services.AddOpenApi();


    var app = builder.Build();
    if (app.Environment.IsDevelopment())
        app.MapOpenApi();
    
    // Use Cors
    app.UseCors("AllowAll");

    // app.UseHttpsRedirection();
    app.MapReverseProxy();


    // Rate Limiter
    app.UseRateLimiter();
    app.MapPrometheusScrapingEndpoint();

    // Health Check
    app.MapGroup("/api/").MapHealthChecks("health");

    app.MapGroup("/api/").AddGatewayEndpoints(builder.Configuration);

    // Security Headers
    app.UseMiddleware<SecurityHeadersMiddleware>();

    StartupMessage.SetStartupMessage(builder.Configuration);
    app.Run();

}
catch (Exception ex)
{
    Console.WriteLine($"gamex API Gateway has been terminated. Reason:" + ex.GetAllExceptions());
    Log.Fatal(ex.GetAllExceptions());    
}
finally{
    Log.CloseAndFlush();
}
