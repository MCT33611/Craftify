using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Craftify.Infrastructure.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CraftifyDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(CraftifyDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includedProperties = null, bool tracked = true)
        {
            IQueryable<T> query = tracked ? _dbSet : _dbSet.AsNoTracking();

            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includedProperties))
            {
                foreach (var includedProp in includedProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includedProp);
                }
            }

            return query.FirstOrDefault()!;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includedProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includedProperties))
            {
                foreach (var includedProp in includedProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includedProp);
                }
            }

            return [.. query];
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
