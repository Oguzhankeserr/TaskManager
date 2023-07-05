using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
