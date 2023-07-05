using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.CommonModels
{
    public class ActionResponse<T> where T : class
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
    }
}
