using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Dtos
{
    public class DeleteUserDto
    {
        public int ProjectId { get; set; }
        public string AssigneeId { get; set; }
        public string ReporterId { get; set; }
    }
}
