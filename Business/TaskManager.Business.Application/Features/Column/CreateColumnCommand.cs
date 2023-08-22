using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using TaskManager.CommonModels.Repositories;

namespace TaskManager.Business.Application.Features
{
    public class CreateColumnCommandRequest : IRequest<ActionResponse<Domain.Entities.Column>>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }

    public class CreateColumnCommand : IRequestHandler<CreateColumnCommandRequest, ActionResponse<Domain.Entities.Column>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IUserInfoRepository _userInfoRepository;
        readonly IRepository<Domain.Entities.Column> _repository;
        readonly IUnitOfWork<Domain.Entities.Column> _uow;
        readonly GenericService<Domain.Entities.Column> _genericService;

        public CreateColumnCommand(BusinessDbContext businessDbContext, IUserInfoRepository userInfoRepository, GenericService<Domain.Entities.Column> genericService, IRepository<Domain.Entities.Column> repository, IUnitOfWork<Domain.Entities.Column> uow)
        {
            _businessDbContext = businessDbContext;
            _userInfoRepository = userInfoRepository;
            _repository = repository;
            _uow = uow;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Domain.Entities.Column>> Handle(CreateColumnCommandRequest createColumnRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Column> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Column column = new();
            column.Name = createColumnRequest.Name;
            column.ProjectId = createColumnRequest.ProjectId;
            column.Status = true;
            column.CreatedDate = column.UpdatedDate = DateTime.UtcNow;
            column.CreatedByUser = column.UpdatedByUser = _userInfoRepository.User.UserId;

			//await _businessDbContext.Columns.AddAsync(column);
			//await _businessDbContext.SaveChangesAsync();
            _genericService.Add(column);

			response.Data = column;
            response.IsSuccessful = true;
            return response;

        }
    }
}
