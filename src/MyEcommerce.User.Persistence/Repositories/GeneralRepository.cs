
using Microsoft.EntityFrameworkCore;
using MyEcommerce.User.Application.Interfaces.Repository;
using MyEcommerce.User.Domain.Common;
using MyEcommerce.User.Persistence.Contexts;

namespace MyEcommerce.User.Persistence.Repositories;

public class GeneralRepository<TEntity>:IGeneralRepository<TEntity> where TEntity:BaseEntity
{
    protected readonly UserDbContext _dbContext;

    public GeneralRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<TEntity>> GetListAsync(bool includeAllDetails = false, CancellationToken cancellationToken = default)
        {
            var entity = _dbContext.Set<TEntity>().AsQueryable();
            if (typeof(ISoftDeletion).IsAssignableFrom(typeof(TEntity)))
            {
                entity = entity.Where(x => ((ISoftDeletion)x).IsDeleted == false);
            }

            if (includeAllDetails)
            {
                var propertyInfos = entity.ElementType.GetProperties()
                    .Where(x => typeof(CoreEntity).IsAssignableFrom(x.PropertyType))
                    .ToList();

                if (propertyInfos.Count > 0)
                {
                    foreach (var propertyInfo in propertyInfos)
                    {
                        entity = entity.Include(propertyInfo.Name);
                    }
                }
            }

            return entity.ToListAsync(cancellationToken);
        }

    
    public Task<TEntity> GetAsync(Guid key, bool includeAllDetails = false, CancellationToken cancellationToken = default)
        {
            var entity = _dbContext.Set<TEntity>().AsQueryable();
            if (includeAllDetails)
            {
                var propertyInfos = entity.ElementType.GetProperties()
                    .Where(x => typeof(CoreEntity).IsAssignableFrom(x.PropertyType))
                    .ToList();

                if (propertyInfos.Count > 0)
                {
                    foreach (var propertyInfo in propertyInfos)
                    {
                        entity = entity.Include(propertyInfo.Name);
                    }
                }
            }

            return entity.FirstOrDefaultAsync(x => x.Id.Equals(key), cancellationToken: cancellationToken);
        }

        public Task<bool> IsExistAsync(Guid key, CancellationToken cancellationToken = default)
        {
            var entity = _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();
            return entity.AnyAsync(x => x.Id.Equals(key), cancellationToken: cancellationToken);
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            var entity = _dbContext.Set<TEntity>().AsQueryable();
            if (typeof(ISoftDeletion).IsAssignableFrom(typeof(TEntity)))
            {
                entity = entity.Where(x => ((ISoftDeletion)x).IsDeleted == false);
            }

            return entity.CountAsync(cancellationToken);
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            if (typeof(IHasCreation).IsAssignableFrom(typeof(TEntity)))
            {
                ((IHasCreation)entity).CreationTime = DateTime.UtcNow;
                // ((IHasCreation)entity).CreatorId = CurrentUser.GetId();
                ((IHasCreation)entity).CreatorId =Guid.Empty;
            }

            var savedEntry = (await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken)).Entity;
            if (autoSave) await _dbContext.SaveChangesAsync(cancellationToken);
            return savedEntry;
        }

        public async Task InsertManyAsync(List<TEntity> entities, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                if (typeof(IHasCreation).IsAssignableFrom(typeof(TEntity)))
                {
                    ((IHasCreation)entity).CreationTime = DateTime.UtcNow;
                    ((IHasCreation)entity).CreatorId = Guid.Empty;
                    // ((IHasCreation)entity).CreatorId = CurrentUser.GetId();
                }
            }

            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            if (autoSave) await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            if (typeof(IHasUpdate).IsAssignableFrom(typeof(TEntity)))
            {
                ((IHasUpdate)entity).UpdateTime = DateTime.UtcNow;
                ((IHasUpdate)entity).UpdateId = Guid.Empty;
                // ((IHasUpdate)entity).UpdateId = CurrentUser.GetId();
            }

            var updatedEntry = _dbContext.Set<TEntity>().Update(entity).Entity;
            if (autoSave) await _dbContext.SaveChangesAsync(cancellationToken);
            return updatedEntry;
        }

        public async Task UpdateManyAsync(List<TEntity> entities, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                if (typeof(IHasUpdate).IsAssignableFrom(typeof(TEntity)))
                {
                    ((IHasUpdate)entity).UpdateTime = DateTime.UtcNow;
                    ((IHasUpdate)entity).UpdateId = Guid.Empty;
                    // ((IHasUpdate)entity).UpdateId = CurrentUser.GetId();
                }
            }

            _dbContext.Set<TEntity>().UpdateRange(entities);
            if (autoSave) await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            if (typeof(ISoftDeletion).IsAssignableFrom(typeof(TEntity)))
            {
                ((ISoftDeletion)entity).DeletionTime = DateTime.UtcNow;
                ((ISoftDeletion)entity).IsDeleted = true;
            }

            var updatedEntry = _dbContext.Set<TEntity>().Update(entity).Entity;
            if (autoSave) await _dbContext.SaveChangesAsync(cancellationToken);
            return updatedEntry;
        }

        public async Task DeleteManyAsync(List<TEntity> entities, bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                if (typeof(ISoftDeletion).IsAssignableFrom(typeof(TEntity)))
                {
                    ((ISoftDeletion)entity).DeletionTime = DateTime.UtcNow;
                    ((ISoftDeletion)entity).IsDeleted = true;
                }
            }

            _dbContext.Set<TEntity>().UpdateRange(entities);
            if (autoSave) await _dbContext.SaveChangesAsync(cancellationToken);
        }

      

        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }
}