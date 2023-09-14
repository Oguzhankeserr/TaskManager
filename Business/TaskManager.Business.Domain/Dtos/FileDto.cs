using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Dtos
{
    public class FileDto
    {
        public int projectId {  get; set; }
        public string path { get; set; }
        public string userId { get; set; }
    }

    public class FileUserDto
    {
        public string path { get; set; }
        public string userId { get; set; }
    }
}
