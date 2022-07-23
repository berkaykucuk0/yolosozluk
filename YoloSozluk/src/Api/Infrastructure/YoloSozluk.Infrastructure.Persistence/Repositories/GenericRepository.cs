using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Api.Domain.Entities.Base;
using YoloSozluk.Infrastructure.Persistence.Context;

namespace YoloSozluk.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
    {
        private readonly DbContext dbContext;


        //Her seferinde dbContext.set<> yazmamak için 
        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        #region add
        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return dbContext.SaveChanges();
        }

        public virtual int Add(IEnumerable<TEntity> entity)
        {
            this.entity.AddRange(entity);
            return dbContext.SaveChanges();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entity)
        {
            await this.entity.AddRangeAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        #endregion
        #region update

        public virtual int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return dbContext.SaveChanges();
        }

        public virtual  async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return await dbContext.SaveChangesAsync();
        }

        #endregion
        #region delete
        public virtual int Delete(Guid id)
        {
            var entity = this.entity.Find(id);
            return Delete(entity);
        }

        public virtual int Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);
            return dbContext.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await this.entity.FindAsync(id);
            return await DeleteAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity) 
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(entity.Where(predicate));
            return dbContext.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(entity.Where(predicate));
            return await dbContext.SaveChangesAsync() > 0;
        }

        #endregion
        #region add or update
        public virtual int AddOrUpdate(TEntity entity)
        {
            //gelen entry yakın zamanda memory de bulunuyor mu diye kontrol ediyoruz. Performans arttırma amaçlı zorunlu değil 
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                dbContext.Update(entity);

            return dbContext.SaveChanges();
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            //gelen entry yakın zamanda memory de bulunuyor mu diye kontrol ediyoruz. Performans arttırma amaçlı zorunlu değil 
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                dbContext.Update(entity);

            return await dbContext.SaveChangesAsync();
        }
        #endregion
        #region get

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy !=null)
            {
                query = orderBy(query);
            }

            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var data = await entity.FindAsync(id);

            if (data == null)
                return null;

            if (noTracking)
                dbContext.Entry(data).State = EntityState.Detached;

            foreach (var include in includes)
            {
                dbContext.Entry(data).Reference(include).Load();
            }

            return data;
        }
        public virtual IQueryable<TEntity> AsQueryAble() => entity.AsQueryable();

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query.Where(predicate);
            }
                

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync() ;
        }

        public virtual async Task<List<TEntity>> GetAllAsync(bool noTracking = true)
        {
            var query = entity.AsQueryable();
            
            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
                query.Where(predicate);

            
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        #endregion
        #region bulk add,update,delete
        public virtual async  Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if (entities == null && !entities.Any())
                await Task.CompletedTask;

            await entity.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }
        public virtual Task BulkDelete(IEnumerable<TEntity> entities)
        {
            if (entities == null && !entities.Any())
                return Task.CompletedTask;

            dbContext.RemoveRange(entities);
            return dbContext.SaveChangesAsync();
        }
        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids == null && !ids.Any())
                return Task.CompletedTask;

            dbContext.RemoveRange(entity.Where(i => ids.Contains(i.Id)));
            return dbContext.SaveChangesAsync();
        }
        public virtual async Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            if (entities == null && !entities.Any())
                await Task.CompletedTask;

            foreach (var item in entities)
            {
                this.entity.Attach(item);
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            await dbContext.SaveChangesAsync();
        }
        #endregion

    }
}
