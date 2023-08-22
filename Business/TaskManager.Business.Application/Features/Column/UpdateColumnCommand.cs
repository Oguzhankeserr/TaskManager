using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using TaskManager.CommonModels.Repositories;

namespace TaskManager.Business.Application.Features.Column
{
    public class UpdateColumnCommandRequest : IRequest<ActionResponse<Domain.Entities.Column>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateColumnCommand : IRequestHandler<UpdateColumnCommandRequest, ActionResponse<Domain.Entities.Column>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IUserInfoRepository _userInfoRepository;
        readonly GenericService<Domain.Entities.Column> _genericService;

        public UpdateColumnCommand(BusinessDbContext businessDbContext, IUserInfoRepository userInfoRepository, GenericService<Domain.Entities.Column> genericService)
        {
            _businessDbContext = businessDbContext;
            _userInfoRepository = userInfoRepository;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Domain.Entities.Column>> Handle(UpdateColumnCommandRequest updateColumnRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Column> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Column column = await _businessDbContext.Columns.FirstOrDefaultAsync(c => c.Id == updateColumnRequest.Id);

            if(column != null && column.Status == true)
            {
                column.Name = updateColumnRequest.Name;
                column.UpdatedDate = DateTime.UtcNow;
                column.UpdatedByUser = _userInfoRepository.User.UserId;
                //await _businessDbContext.SaveChangesAsync();
                _genericService.Update(column);
                response.Data = column;
                response.IsSuccessful = true;
            }
            return response;
        }
    }
}
