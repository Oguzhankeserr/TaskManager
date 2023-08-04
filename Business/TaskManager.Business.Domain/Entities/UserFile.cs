using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class UserFile : File
    {
        public int UserId { get; set; }
    }
}
