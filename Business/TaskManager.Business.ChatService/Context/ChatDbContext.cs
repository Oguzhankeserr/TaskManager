using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace TaskManager.Business.ChatService.Context
{
	public class ChatDbContext : DbContext
	{
		public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }
		//public DbSet<Ta> Chats { get; set; }
	}
}
