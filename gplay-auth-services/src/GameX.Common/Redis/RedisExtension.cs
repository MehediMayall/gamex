using StackExchange.Redis;

namespace gamex.Common;

public static class RedisExtension
{
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration){
        var redisSettings = configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

        LogAndThrowException.IfNull(redisSettings, $"Trying to load RedisSettings Configuration but not found in appsettings.json");
        

        // Configure Redis
        var redisOptions = new ConfigurationOptions
        {
            EndPoints = { redisSettings.Host },
            AbortOnConnectFail = false, // Prevent application crash on Redis connection issues
            KeepAlive = 60,            // Set keep-alive for idle connections
            SyncTimeout = 5000,        // Increase timeout for sync operations
            ConnectRetry = 5,          // Retry attempts for connection
            ReconnectRetryPolicy = new ExponentialRetry(5000), // Retry policy for reconnection
            // Password = "yourpassword"  // Set if Redis is password-protected
        };

        var redisConnection = ConnectionMultiplexer.Connect(redisOptions);
        services.AddSingleton<IConnectionMultiplexer>(redisConnection);

        Log.Information("REDIS CONNECTED SUCCESSFULLY");
    }
}