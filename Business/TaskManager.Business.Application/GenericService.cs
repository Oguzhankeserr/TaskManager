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

        public void Update(T entity)
        {
            try
            {
                _repository.Update(entity);
                _unitOfWork.CommitAsync();
            }
            catch 
            {
                _unitOfWork.RollbackAsync();  
            }
        
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                _repository.UpdateRange(entities);
                _unitOfWork.CommitAsync();  
            }
            catch 
            {
                _unitOfWork.RollbackAsync();
            }
        }

        public void Remove(T entity)
        {
            try
            {
              _repository.Remove(entity);
              _unitOfWork.CommitAsync();
            }
            catch 
            {
                _unitOfWork.RollbackAsync();
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
               _repository.RemoveRange(entities);
               _unitOfWork.CommitAsync();
            }
            catch 
            {
                _unitOfWork.RollbackAsync();
            }
        }

        public void GetAll() // Kontrol edilecek 
        {
            try
            {
                _repository.GetAll();
                _unitOfWork.Rollback();
            }
            catch 
            {
                _unitOfWork.RollbackAsync();
            }
        }
        public async Task GetAllAsync()
        {
            try
            {
                await _repository.GetAllAsync();
                await _unitOfWork.CommitAsync();
            }
            catch 
            {
                await _unitOfWork.RollbackAsync();
            }
        }

    }
}
