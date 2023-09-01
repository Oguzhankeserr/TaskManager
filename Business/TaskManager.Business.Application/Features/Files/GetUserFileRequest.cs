using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
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
    public class GetUserFileRequest : IRequest<List<string>>
    {
        public string UserId { get; set; }
    }

    public class GetUserFile : IRequestHandler<GetUserFileRequest, List<string>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IConfiguration _configuration;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IStorageService _storageService;

        public GetUserFile(BusinessDbContext businessDbContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IStorageService storageService)
        {
            _businessDbContext = businessDbContext;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
        }

        public async Task<List<string>> Handle(GetUserFileRequest getUserFileRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<UserFile>> response = new();
            response.IsSuccessful = false;

            string query = @" SELECT path, userid FROM files 
            WHERE userid = @UserId";

            var x = _businessDbContext.ExecuteQuery<UserFile>(query, new { UserId = getUserFileRequest.UserId});
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
