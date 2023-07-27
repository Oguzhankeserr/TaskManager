using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.Abstractions.Storage;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Files
{
    public class UploadTaskFileCommandRequest : IRequest<ActionResponse<TaskFile>>
    {
        public int TaskId { get; set; }
        public IFormFile? Files { get; set; }
    }
    public class UploadTaskFileCommand : IRequestHandler<UploadTaskFileCommandRequest,ActionResponse<TaskFile>>
    {
        readonly IStorageService _storageService;
        readonly BusinessDbContext _businessDbContext;

        public UploadTaskFileCommand(BusinessDbContext businessDbContext, IStorageService storageService)
        {
            _businessDbContext = businessDbContext;
            _storageService = storageService;
        }

        public async Task<ActionResponse<TaskFile>> Handle(UploadTaskFileCommandRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<TaskFile> response = new();
            response.IsSuccessful = true;
            try
            {
                List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);

                foreach (var item in result)
                {
                    TaskFile taskFile = new();
                    taskFile.FileName = item.fileName;
                    taskFile.Path = item.pathOrContainerName;
                    taskFile.Storage = _storageService.StorageName;
                    taskFile.TaskId = request.TaskId;
                   await _businessDbContext.Files.AddAsync(taskFile);

                }

                await _businessDbContext.SaveChangesAsync();
                //response.Data = datas;
                
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
