﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Business.Infrastructure.Context;

namespace TaskManager.Business.Application
{
    public class GenericService<T> where T : class
    {
        private readonly IUnitOfWork<T> _unitOfWork;
        private readonly IRepository<T> _repository;
        private readonly BusinessDbContext  _businessDbContext;

        public GenericService(IUnitOfWork<T> unitOfWork, IRepository<T> repository, BusinessDbContext businessDbContext)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _businessDbContext = businessDbContext;
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
        public async void AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
            }

        }

        public async void Update(T entity)
        {
            try
            {
                _repository.Update(entity);
                await _unitOfWork.CommitAsync();
            }
            catch 
            {
                await _unitOfWork.RollbackAsync();  
            }
        
        }

        public async void UpdateRange(IEnumerable<T> entities)
        {
            try
            {
                _repository.UpdateRange(entities);
                await _unitOfWork.CommitAsync();  
            }
            catch 
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async void UpdateRangeList(List<T> entities)
        {
            try
            {
                _repository.UpdateRangeList(entities);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async void Remove(T entity)
        {
            try
            {
              _repository.Remove(entity);
              await _unitOfWork.CommitAsync();
            }
            catch 
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                _repository.RemoveRange(entities);
               await _unitOfWork.CommitAsync();
            }
            catch 
            {
                await _unitOfWork.RollbackAsync();
            }
        }

        public async void GetAll() // Kontrol edilecek 
        {
            try
            {
                _repository.GetAll();
                _unitOfWork.Rollback();
            }
            catch 
            {
                await _unitOfWork.RollbackAsync();
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
