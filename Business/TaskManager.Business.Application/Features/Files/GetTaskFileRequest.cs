using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
    public class GetTaskFileRequest : IRequest<List<string>>
    {
        public int TaskId { get; set; }
    }

    public class GetTaskFile : IRequestHandler<GetTaskFileRequest, List<string>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IConfiguration _configuration;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IStorageService _storageService;

        public GetTaskFile(BusinessDbContext businessDbContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IStorageService storageService)
        {
            _businessDbContext = businessDbContext;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
        }

        public async Task<List<string>> Handle(GetTaskFileRequest getTaskFileRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskFile>> response = new();
            response.IsSuccessful = false;

            string query = @" SELECT path, taskId FROM files 
            WHERE taskId = @TaskId";

            var x = _businessDbContext.ExecuteQuery<TaskFile>(query, new { TaskId = getTaskFileRequest.TaskId });
            response.Data = x.ToList();
            response.IsSuccessful = true;


            List<String> filePaths = new();

            foreach (var item in x)
            {
                filePaths.Add(item.Path);
            }

            return filePaths;


        }



    }
}
