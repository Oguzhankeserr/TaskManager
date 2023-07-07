using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class Column : BaseEntity
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
