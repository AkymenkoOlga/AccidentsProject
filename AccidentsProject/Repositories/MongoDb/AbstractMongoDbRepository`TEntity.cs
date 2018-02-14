using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentsProject.Repositories.Entities;
using MongoDB.Driver;

namespace AccidentsProject.Repositories.MongoDb
{
    public class AbstractMongoDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
    {
        protected AbstractMongoDbRepository(IMongoClient client, string databaseName, string collectionName)
        {
            this.Collection = client
                .GetDatabase(databaseName)
                .GetCollection<TEntity>(collectionName);
        }

        protected IMongoCollection<TEntity> Collection { get; }

        public async Task InsertOneAsync(TEntity entity)
        {
            await this.Collection
                .InsertOneAsync(entity)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> RetrieveAllAsync()
        {
            return await this.Collection
                .Find(_ => true)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<TEntity> RetrieveOneAsync(string entityId)
        {
            return await this.Collection
                .Find(entity => entityId.Equals(entity.Id))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task DeleteOneAsync(string entityId)
        {
            DeleteResult result = await this.Collection
                .DeleteOneAsync(e => e.Id.Equals(entityId))
                .ConfigureAwait(false);

            if (!result.IsAcknowledged)
            {
                throw new InvalidOperationException($"Unable to delete entity with entityId {entityId} from database.");
            }
        }

        public async Task UpdateOneOrThrowArgumentExceptionAsync(TEntity entity)
        {
            ReplaceOneResult result = await this.Collection
                .ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity)
                .ConfigureAwait(false);

            if (result.MatchedCount == 0)
            {
                throw new ArgumentException($"Unable to find entity with entityId {entity.Id} in database.");
            }

            if (!result.IsAcknowledged)
            {
                throw new InvalidOperationException($"Unable to update entity with entityId {entity.Id} in database.");
            }
        }

        public async Task<TEntity> RetrieveOneOrThrowArgumentExceptionAsync(string entityId)
        {
            TEntity entity = await RetrieveOneAsync(entityId).ConfigureAwait(false);

            if (entity == null)
            {
                throw new ArgumentException($"Unable to retrieve entity with entityId {entityId} from database.");
            }
            return entity;
        }
    }
}