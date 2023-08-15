using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Comment { get; set; }
        public bool Rewrite { get; set; }
        public bool Status { get; set; }
        public Guid CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}


