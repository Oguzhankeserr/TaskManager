using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Dtos
{
    public class ProjectUserDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
       // public string? ProjectName { get; set; }
        public string UserId { get; set; }
       // public string? UserName { get; set; }
       // public string ProjectRole { get; set; }
        //public string UserImage { get; set; }

    }
}
