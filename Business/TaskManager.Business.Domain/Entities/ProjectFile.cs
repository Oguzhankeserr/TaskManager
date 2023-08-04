using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class ProjectFile :File
    {
        public int ProjectId { get; set; }
    }
}
