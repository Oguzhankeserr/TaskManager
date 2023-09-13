using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Identity.Domain.Entities
{
    public class AppUser: IdentityUser<string>
    {
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Status { get; set; }
    }
}
