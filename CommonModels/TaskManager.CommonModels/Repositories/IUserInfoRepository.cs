using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Entities;

namespace TaskManager.CommonModels.Repositories
{
    public interface IUserInfoRepository
    {
        public UserInfo User { get; }
    }
}
