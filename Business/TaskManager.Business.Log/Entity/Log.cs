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
        
        //public string MasterId { get; set; }



    }
}


/*
                                    og.OldValue = originalValue;
                                    log.NewValue = updatedValue;
                                    log.FieldName = property.Name;
                                    log.TableName = entity.Entity.GetType().Name;
                                    log.ActionDate = now;
                                    log.UserEmail = _userRepository.User.Email;
                                    log.TableId = id;
                                    log.MasterId = id;
                                    _mediator.Send(log);

 
 */