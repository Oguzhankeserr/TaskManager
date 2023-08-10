using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Identity.Application.Models
{
	public class PasswordToken
	{
        public string PasswordTokenAccess { get; set; }
		public DateTime Expiration { get; set; }

	}
}
