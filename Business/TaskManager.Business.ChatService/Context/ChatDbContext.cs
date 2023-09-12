using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TaskManager.Business.Chat.Entity;

namespace TaskManager.Business.ChatService.Context
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }
        public DbSet<Business.Chat.Entity.Chat> Chats { get; set; }
       
    }
}
