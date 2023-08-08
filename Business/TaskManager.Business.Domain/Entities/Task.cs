using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int ColumnId { get; set; }
        public int Priority { get; set; }
        public DateTime UserUpdatedDate { get; set; }  // the change made by the user to whom the task is assigneds
        // in base entity there is another updated date which related with admin's update.
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public string AssigneeId { get; set; }
        public string ReporterId { get; set; }
        public Boolean IsDone { get; set; }
        //public string AssignedTime { get; set; }
    }
}
