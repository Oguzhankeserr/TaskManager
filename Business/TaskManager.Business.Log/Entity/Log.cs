using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Log.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int TableId { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ActionDate { get; set; }
        public string UserId { get; set; } // email?
        public int ProjectId { get; set; }



    }
}

