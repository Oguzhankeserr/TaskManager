﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Business.Log.Dto
{
    public class LogDto
    {   
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ActionDate { get; set; }
        public string UserId { get; set; }
    }
}
