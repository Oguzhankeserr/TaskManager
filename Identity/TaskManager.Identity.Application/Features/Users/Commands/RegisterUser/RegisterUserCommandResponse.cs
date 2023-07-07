using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Identity.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
