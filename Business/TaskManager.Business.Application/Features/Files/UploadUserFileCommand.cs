using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Abstractions.Storage;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Files
{
    public class UploadUserFileCommand : IRequest<ActionResponse<UserFile>>
    {
        public string UserId { get; set; }
        public List<IFormFile>? Files { get; set; }
    }

    public class UploadUserFileCommandRequest : IRequestHandler<UploadUserFileCommand, ActionResponse<UserFile>>
    {
        readonly IStorageService _storageService;
        readonly BusinessDbContext _businessDbContext;

        public UploadUserFileCommandRequest(BusinessDbContext businessDbContext, IStorageService storageService)
        {
            _businessDbContext = businessDbContext;
            _storageService = storageService;
        }

        public async Task<ActionResponse<UserFile>> Handle(UploadUserFileCommand request, CancellationToken cancellationToken)
        {
            ActionResponse<UserFile> response = new();
            response.IsSuccessful = true;
            try
            {
                List<string> paths = await _storageService.UploadAsync("files", request.Files);

                
                foreach (var path in paths)
                {
                    
                    UserFile userFile = new()
                    { 
                        Path = path,
                        Storage = _storageService.StorageName,
                        UserId = request.UserId
                    };
                    await _businessDbContext.Files.AddAsync(userFile);
                    
                }

                await _businessDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = string.Format("An Error Occured. Message is {0}", ex.Message);
            }
            return response;
        }
    }
}
