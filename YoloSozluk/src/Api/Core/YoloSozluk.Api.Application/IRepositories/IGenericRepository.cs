using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Domain.Entities.Base;

namespace YoloSozluk.Api.Application.IRepositories
{
    public interface IGenericRepository<TEntity>  where TEntity : BaseModel
    {

        Task<int> AddAsync(TEntity entity);
        Task<int> AddAsync(IEnumerable<TEntity> entity);
        int Add(TEntity entity);
        int Add(IEnumerable<TEntity> entity);


        Task<int> UpdateAsync(TEntity entity);
        int Update(TEntity entity);

        Task<int> DeleteAsync(Guid id );
        int Delete(Guid id);
        Task<int>DeleteAsync(TEntity entity);
        int Delete(TEntity entity);
        Task<bool> DeleteRangeAsync(Expression<Func<TEntity,bool>> predicate);
        bool DeleteRange(Expression<Func<TEntity, bool>> predicate);


        Task<int> AddOrUpdateAsync(TEntity entity);
        int AddOrUpdate(TEntity entity);

        IQueryable<TEntity> AsQueryAble();
        Task<List<TEntity>> GetAllAsync(bool noTracking = true);


        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true,Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>  orderBy = null, params                                                          Expression<Func<TEntity,object>>[] includes);

        Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);

        Task BulkDeleteById(IEnumerable<Guid> ids);
        Task BulkDelete(IEnumerable<TEntity> entities);
        Task BulkUpdate(IEnumerable<TEntity> entities);
        Task BulkAdd(IEnumerable<TEntity> entities);
    }
}
