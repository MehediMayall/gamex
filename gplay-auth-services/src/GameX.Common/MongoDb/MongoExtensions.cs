using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace gamex.Common;

public static class MongoExtensions{

    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration){
        
        ServiceSettings serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();


        // MongoDb Settings
        services.AddSingleton(provider => {
            MongoDbSettings mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var mongoClient = new MongoClient(mongoDbSettings.ConnectionStrings);
            return mongoClient.GetDatabase(serviceSettings.ServiceName);
        });

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        return services;
    }


    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string CollectionName)
        where T : EntityBase
    {
        
        services.AddSingleton<IMongoRepository<T>>(provider =>{
            var database = provider.GetService<IMongoDatabase>();
            return new MongoRepository<T>(database, CollectionName);
        });

        return services;
    }
}