using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Interfaces.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository<TEntity, TId> : IEfRepository<TEntity, TId> where TEntity : class, IModel<TId>
    {
        protected readonly ApplicationDbContext _dbContext;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            var source = GetAll();
            return await source.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecificationQuery<TEntity> spec)
        {
            return await GetAll(spec).ToListAsync();
        }

        public async Task<TEntity> GetAsync(ISpecificationQuery<TEntity> spec)
        {
            return await GetAll(spec).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            var source = GetAll();
            return await source.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<TEntity> GetAsync(ISpecificationQuery<TEntity> spec, TId id)
        {
            return await GetAll(spec)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<int> CountAllAsync()
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .CountAsync();
        }

        public async Task<int> CountAsync(ISpecificationQuery<TEntity> spec)
        {
            return await GetAll(spec).CountAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistDataAsync(IDictionary<string, object> whereData)
        {
            var dbSet = _dbContext.Set<TEntity>().AsNoTracking();

            IQueryable<TEntity> exp;

            var whereCriteria = string.Empty;

            int i = 1;
            foreach (var item in whereData)
            {
                if (item.Value.GetType() == typeof(string) || item.Value.GetType() == typeof(Guid) || item.Value.GetType() == typeof(DateTime))
                {
                    whereCriteria += item.Key + "=\"" + item.Value + "\"";
                }
                else if (item.Value.GetType() == typeof(DateTime))
                {
                    var dateValue = DateTime.Parse(item.Value.ToString());
                    var dateValueAdd = DateTime.Parse(item.Value.ToString()).AddDays(1);

                    whereCriteria += item.Key + " >= DateTime(" + dateValue.Year + ", " + dateValue.Month + ", " + dateValue.Day + ") and " + item.Key + " < DateTime(" + dateValueAdd.Year + ", " + dateValueAdd.Month + ", " + dateValueAdd.Day + ")";
                }
                else
                {
                    whereCriteria += item.Key + "=" + item.Value;
                }

                if (i < whereData.Count)
                {
                    whereCriteria += " and ";
                }

                i++;
            }

            exp = dbSet.Where(whereCriteria);
            var data = await exp.AnyAsync();
            return data;
        }

        public IQueryable<TEntity> GetAll()
        {
            var source = _dbContext.Set<TEntity>().AsNoTracking();

            foreach (var item in EntityIncludes())
            {
                source = source.Include(item);
            }

            return source;
        }

        public IQueryable<TEntity> GetAll(ISpecificationQuery<TEntity> spec)
        {
            var source = GetAll();
            return BaseSpecificationQuery<TEntity>.GetQuery(source, spec);
        }

        protected List<string> EntityIncludes()
        {
            var includes = new List<string>();
            foreach (var property in _dbContext.Model.FindEntityType(typeof(TEntity)).GetNavigations())
            {
                includes.Add(property.Name);
            }

            return includes;
        }
    }
}
