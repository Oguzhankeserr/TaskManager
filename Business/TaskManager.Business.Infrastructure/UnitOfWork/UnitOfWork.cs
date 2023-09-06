using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.Business.LogService.Application;
using TaskManager.CommonModels.Repositories;

namespace TaskManager.Business.Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly BusinessDbContext _businessDbContext;
        private readonly IUserInfoRepository _userInfoRepository;
        readonly IMediator _mediator;

        public UnitOfWork(BusinessDbContext businessDbContext, IUserInfoRepository userInfoRepository, IMediator mediator)
        {
            _businessDbContext = businessDbContext;
            _userInfoRepository = userInfoRepository;
            _mediator = mediator;
        }

        public void Commit() 
            => _businessDbContext.SaveChanges();

        public async Task CommitAsync()
        {
            var updatedEntities = _businessDbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            var now = System.DateTime.UtcNow;
            foreach (var entity in updatedEntities)
            {
                var idProperty = entity.OriginalValues.Properties.Where(x => x.Name == "Id").FirstOrDefault();
               

                foreach (var property in entity.OriginalValues.Properties)
                {
                    if (property.Name == "Id" || property.Name == "CreatedDate" || property.Name == "CreatedByUser"
                            || property.Name == "UpdatedDate" || property.Name == "UpdatedByUser") continue;

                    var originalValue = entity.OriginalValues[property]?.ToString();
                    var updatedValue = entity.CurrentValues[property]?.ToString();
                    if (originalValue != updatedValue)
                    {
                       
                        //var entityType = _businessDbContext.Model.FindEntityType(entity.GetType());
                        //var tableName = entityType.GetTableName();
                        try
                        { 
                            AddLogCommand log = new AddLogCommand();
                            log.OldValue = originalValue;
                            log.NewValue = updatedValue;
                            log.FieldName = property.Name;
                            log.TableName = entity.Entity.GetType().Name;
                            log.ActionDate = now;
                            log.UserId = _userInfoRepository.User.UserId.ToString();
                            log.TableId = entity.OriginalValues.GetValue<int>("Id");

                            _mediator.Send(log);
                        }
                        catch (Exception excc)
                        {

                        }
                    }

                }

            }
                await _businessDbContext.SaveChangesAsync();
        }

        public void Rollback() //Doğru bir kullanım olup olmadığına daha sonra bakacağım
            => _businessDbContext.Dispose();

        public async Task RollbackAsync()
            => await _businessDbContext.DisposeAsync();

    }
}

/*
 
  public string Name { get; set; }
        public int ProjectId { get; set; }
        public int ColumnId { get; set; }
        public int Priority { get; set; }
        public DateTime UserUpdatedDate { get; set; }  // the change made by the user to whom the task is assigneds
        // in base entity there is another updated date which related with admin's update.
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public string AssigneeId { get; set; }
        public string ReporterId { get; set; }
        public int? Label { get; set; }

public int Id { get; set; }
        public Guid CreatedByUser { get; set; }
        public Guid UpdatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
 */
