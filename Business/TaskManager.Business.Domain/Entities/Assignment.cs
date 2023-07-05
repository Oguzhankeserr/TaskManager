using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
    }
}
