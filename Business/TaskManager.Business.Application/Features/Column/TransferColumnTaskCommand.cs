using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManager.Business.Application.Features.Column
{
    public class TransferColumnTaskCommandRequest : IRequest<ActionResponse<ColumnDto>>
    {
        public int ProjectId { get; set; }
        public int ColumnId { get; set; }
        public int TransferredColumnId { get; set; } 
    }


    public class TransferColumnTaskCommand : IRequestHandler<TransferColumnTaskCommandRequest, ActionResponse<ColumnDto>>
    {
        readonly BusinessDbContext _businessDbContext;

        public TransferColumnTaskCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<ColumnDto>> Handle(TransferColumnTaskCommandRequest transferRequest, CancellationToken cancellationToken)
        {
            ActionResponse<ColumnDto> response = new();
            response.IsSuccessful = false;

            
            try
            {
                string query = "UPDATE tasks SET columnid = @TransferredColumnId WHERE columnid = @ColumnId AND projectid = @ProjectId";

                var parameters = new NpgsqlParameter[]
                {
                   new NpgsqlParameter("@TransferredColumnId", NpgsqlDbType.Integer) { Value = transferRequest.TransferredColumnId },
                   new NpgsqlParameter("@ColumnId", NpgsqlDbType.Integer) { Value = transferRequest.ColumnId },
                   new NpgsqlParameter("@ProjectId", NpgsqlDbType.Integer) { Value = transferRequest.ProjectId }
                };

                await _businessDbContext.Database.ExecuteSqlRawAsync(query, parameters);

                response.IsSuccessful = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccessful = false;
                return response;
            }

        }
    }
}
