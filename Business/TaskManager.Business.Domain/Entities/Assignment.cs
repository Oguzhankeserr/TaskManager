﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Domain.Entities
{
    public class Assignment 
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int ColumnId { get; set; }
        public int TaskId { get; set; }
        public string AssigneeId { get; set; }
        public string ReporterId { get; set; }
        public DateTime AssignTime { get; set; }
        public bool Status { get; set; }
    }
}
