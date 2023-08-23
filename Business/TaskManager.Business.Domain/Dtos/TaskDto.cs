using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColumnId { get; set; }
        public int Priority { get; set; }

        public int Label { get; set; }
    }

    public class TaskListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColumnId { get; set; }
        public int Priority { get; set; }
        public string AssigneeId { get; set;}
        public string ReporterId { get; set; }
        public bool IsDone { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
    public class TaskUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int Priority { get; set; }
        public string AssigneeId { get; set; }

        //public int AssigneeId { get; set; }
        //public int ReporterId { get; set; }
    }

}
