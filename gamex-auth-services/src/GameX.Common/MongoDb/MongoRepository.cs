using System.Linq.Expressions;
using MongoDB.Driver;

namespace gamex.Common;


public class MongoRepository<T> : IMongoRepository<T> where T : EntityBase{


    private readonly IMongoCollection<T> dbCollection;
    private readonly FilterDefinitionBuilder<T> filterBuilder = new();

    public MongoRepository(IMongoDatabase database, string CollectionName)
    {        
        dbCollection = database.GetCollection<T>(CollectionName);
    }

    public async Task<IReadOnlyCollection<T>> GetAll() {
        return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }
    
    public async Task<IReadOnlyCollection<T>> GetAll(Expression<Func<T, bool>> filter) {
        return await dbCollection.Find(filter).ToListAsync();
    }

    public async Task<T> Get(Guid Id) {
        FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, Id);
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> filter) {
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task Create(T item) {
        await dbCollection.InsertOneAsync(item);
    }

    public async Task Update(T item) {
        FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, item.Id);
        await dbCollection.ReplaceOneAsync(filter, item);
    }

    public async Task Delete(Guid Id) {
        FilterDefinition<T> filter = filterBuilder.Eq(e=> e.Id, Id);
        await dbCollection.DeleteOneAsync(filter);
    }

}