using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    public class GetFileForProjectUsersRequest : IRequest<List<FileUserDto>>
    {
        public int ProjectId { get; set; }
    }

    public class GetFileForProjectUsers : IRequestHandler<GetFileForProjectUsersRequest, List<FileUserDto>>
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

        public async Task<List<FileUserDto>> Handle(GetFileForProjectUsersRequest getFileForProjectUsersRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<UserFile>> response = new();
            response.IsSuccessful = false;
          
            List<FileUserDto> filePaths = new();
            

            string q = @"SELECT p.userid, f.path , f.discriminator FROM files f JOIN projectusers p ON p.userid = f.userid WHERE p.projectid = @ProjectId AND f.discriminator = 'UserFile'";
            string queries = @"SELECT p.userid, f.path, f.discriminator, f.id
                    FROM projectusers p
                    JOIN files f ON p.userid = f.userid
                    WHERE p.projectid = 2
                     AND f.discriminator = 'UserFile'
                        AND f.id = (
                        SELECT MAX(id)
                        FROM files
                            WHERE userid = p.userid
                     AND discriminator = 'UserFile')";


            var x = _businessDbContext.ExecuteQuery<FileDto>(queries, new { projectId = getFileForProjectUsersRequest.ProjectId });
            foreach (var item in x)
            {
                if(item.userId != null)
                {
                    FileUserDto u = new FileUserDto();
                    u.userId = item.userId;
                    u.path = item.path;
                    filePaths.Add(u);
                }
                
            }


            
            



            return filePaths;


        }


    }
}