using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.Context;

namespace TaskManager.Business.Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly BusinessDbContext _businessDbContext;

        public UnitOfWork(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public void Commit()
            => _businessDbContext.SaveChanges();

        public async Task CommitAsync()
            => await _businessDbContext.SaveChangesAsync();

        public void Rollback() //Doğru bir kullanım olup olmadığına daha sonra bakacağım
            => _businessDbContext.Dispose();

        public async Task RollbackAsync()
            => await _businessDbContext.DisposeAsync();

    }
}
