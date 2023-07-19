using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain;
using TaskManager.Business.Infrastructure.Context;

namespace TaskManager.Business.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
            protected readonly BusinessDbContext _businessDbContext;
            private readonly DbSet<T> _entitiySet;


            public Repository(BusinessDbContext businessDbContext)
            {
            _businessDbContext = businessDbContext;
                _entitiySet = _businessDbContext.Set<T>();
            }


            public void Add(T entity)
                => _businessDbContext.Add(entity);


            public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
                => await _businessDbContext.AddAsync(entity, cancellationToken);


            public void AddRange(IEnumerable<T> entities)
                => _businessDbContext.AddRange(entities);


            public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
                => await _businessDbContext.AddRangeAsync(entities, cancellationToken);


            public T Get(Expression<Func<T, bool>> expression)
                => _entitiySet.FirstOrDefault(expression);


            public IEnumerable<T> GetAll()
                => _entitiySet.AsEnumerable();


            public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
                => _entitiySet.Where(expression).AsEnumerable();


            public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
                => await _entitiySet.ToListAsync(cancellationToken);


            public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
                => await _entitiySet.Where(expression).ToListAsync(cancellationToken);


            public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
                => await _entitiySet.FirstOrDefaultAsync(expression, cancellationToken);


            public void Remove(T entity)
                => _businessDbContext.Remove(entity);


            public void RemoveRange(IEnumerable<T> entities)
                => _businessDbContext.RemoveRange(entities);


            public void Update(T entity)
                => _businessDbContext.Update(entity);


            public void UpdateRange(IEnumerable<T> entities)
                => _businessDbContext.UpdateRange(entities);
    }   
}
