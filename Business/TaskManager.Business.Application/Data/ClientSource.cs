using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Application.Models;

namespace TaskManager.Business.Application.Data
{
	public class ClientSource
	{
		public static List<Client> Clients { get; } = new List<Client>();
	}
}
