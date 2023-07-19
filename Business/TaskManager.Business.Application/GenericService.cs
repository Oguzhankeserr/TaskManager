using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Domain;

namespace TaskManager.Business.Application
{
    public class GenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<T> _repository;

        public GenericService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void Add(T entity)
        {
            try
            {
                _repository.Add(entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
        }
        public void AddAsync(T entity)
        {
            try
            {
                _repository.AddAsync(entity);
                _unitOfWork.CommitAsync();
            }
            catch
            {
                _unitOfWork.RollbackAsync();
            }

        }

    }
}
