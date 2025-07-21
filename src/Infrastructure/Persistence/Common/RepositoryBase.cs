using System.Linq.Expressions;
using Application.Common.Abstractions;
using Domain.Abstractions;
using Humanizer;
using Infrastructure.Common.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Common;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : IHasUuid
{
    private readonly IMongoCollection<TEntity> _mongoDbCollection;

    protected RepositoryBase(IOptions<AppOptions> appOptions)
    {
        var databaseSettings = appOptions.Value.MongoDbOptions;

        var mongoClient = new MongoClient(databaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.DatabaseName);

        var collectionName = typeof(TEntity).Name.Pluralize();

        _mongoDbCollection = mongoDatabase.GetCollection<TEntity>(collectionName);
    }

    public virtual async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _mongoDbCollection.InsertOneAsync(entity, null, cancellationToken);

        var filterDefinition = Builders<TEntity>.Filter.Eq(document => document.Uuid, entity.Uuid);

        var documentCursor =
            await _mongoDbCollection.FindAsync<TEntity>(filterDefinition, cancellationToken: cancellationToken);

        return await documentCursor.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition =
            filter is null ? FilterDefinition<TEntity>.Empty : Builders<TEntity>.Filter.Where(filter);

        var documentCursor = await _mongoDbCollection
            .FindAsync(filterDefinition,
                cancellationToken: cancellationToken);

        return await documentCursor.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByUuidAsync(Guid uuid,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<TEntity>.Filter.Eq(document => document.Uuid, uuid);

        var findOptions = new FindOptions<TEntity>
        {
            Limit = 1
        };

        var documentCursor = await _mongoDbCollection.FindAsync(
            filterDefinition,
            findOptions,
            cancellationToken);

        return await documentCursor.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = new FilterDefinitionBuilder<TEntity>()
            .Eq(document => document.Uuid, entity.Uuid);

        var result = await _mongoDbCollection.FindOneAndReplaceAsync(filterDefinition, entity, null, cancellationToken);

        return result;
    }

    public virtual async Task<TEntity?> DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var filterDefinition = new FilterDefinitionBuilder<TEntity>()
            .Where(document => document.Uuid == entity.Uuid);

        var result = await _mongoDbCollection.FindOneAndDeleteAsync(filterDefinition, null, cancellationToken);

        return result;
    }

    public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var idsToDelete = entities.Select(entity => entity.Uuid).ToList();

        await _mongoDbCollection.DeleteManyAsync(
            entity => idsToDelete.Contains(entity.Uuid),
            cancellationToken);
    }
}