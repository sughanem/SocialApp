using System.Collections.Generic;
using System.Threading.Tasks;
using SocialApp.Database;

namespace SocialAppService.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> CreateAsync(TEntity t);
        Task<IEnumerable<TEntity>> RetrieveAllAsync();
        Task<TEntity> RetrieveAsync(int id);
        Task<TEntity> UpdateAsync(int id, TEntity t);
        Task<bool?> DeleteAsync(int id);
        bool ifExisting(int id);
        SocialAppDatabase getDbContext();
    }
}