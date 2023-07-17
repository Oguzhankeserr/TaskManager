using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Dtos
{
    public class ColumnTaskDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}
