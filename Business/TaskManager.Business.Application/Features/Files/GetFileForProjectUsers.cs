using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Abstractions.Storage;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Files
{
    public class GetFileForProjectUsersRequest : IRequest<List<string>>
    {
        public int ProjectId { get; set; }
    }

    public class GetFileForProjectUsers : IRequestHandler<GetFileForProjectUsersRequest, List<string>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IConfiguration _configuration;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IStorageService _storageService;

        public GetFileForProjectUsers(BusinessDbContext businessDbContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IStorageService storageService)
        {
            _businessDbContext = businessDbContext;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
        }

        public async Task<List<string>> Handle(GetFileForProjectUsersRequest getFileForProjectUsersRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<UserFile>> response = new();
            response.IsSuccessful = false;
            List<string> usersId = new();
            List<UserFile> y = new();
            List<String> filePaths = new();

            

            string q = @"SELECT p.userid, f.path FROM files f JOIN projectusers p ON p.userid = f.userid 
WHERE p.projectid = @ProjectId AND f.discriminator = 'UserFile' ";



            var x = _businessDbContext.ExecuteQuery<FileDto>(q, new { projectId = getFileForProjectUsersRequest.ProjectId });
            foreach (var item in x)
            {
                filePaths.Add(item.path);
            }


            
            



            return filePaths;


        }


    }
}