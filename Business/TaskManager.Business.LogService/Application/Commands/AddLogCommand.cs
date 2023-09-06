using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.LogService.Context;

namespace TaskManager.Business.LogService.Application.Commands
{
    public class AddLogCommand : IRequest<bool>
    {
        public string TableName { get; set; }
        public int TableId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ActionDate { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
    }

    public class AddLogCommandHandler : IRequestHandler<AddLogCommand, bool>
    {
        readonly LogDbContext _logDbContext;

        public AddLogCommandHandler(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
        }

        public async Task<bool> Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var log = new Log.Entity.Log
                {
                    TableName = request.TableName,
                    TableId = request.TableId,
                    FieldName = request.FieldName,
                    OldValue = request.OldValue,
                    NewValue = request.NewValue,
                    ActionDate = request.ActionDate,
                    UserId = request.UserId,
                    ProjectId = request.ProjectId

                    
                };

                _logDbContext.Logs.Add(log);
                await _logDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
