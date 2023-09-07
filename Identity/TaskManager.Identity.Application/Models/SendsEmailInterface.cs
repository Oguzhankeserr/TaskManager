using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Dtos;

namespace TaskManager.Identity.Application.Models
{
    public class SendsEmailInterface
    {
        public string Message { get; set; }  
        public List<string> Users { get; set; } 
    }
}
