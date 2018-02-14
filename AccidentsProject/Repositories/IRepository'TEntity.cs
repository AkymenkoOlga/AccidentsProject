using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentsProject.Repositories.Entities;

namespace AccidentsProject.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        Task InsertOneAsync(TEntity entity);

        Task<IEnumerable<TEntity>> RetrieveAllAsync();

        Task<TEntity> RetrieveOneOrThrowArgumentExceptionAsync(string entityId);

        Task UpdateOneOrThrowArgumentExceptionAsync(TEntity entity);

        Task DeleteOneAsync(string entityId);
    }
}