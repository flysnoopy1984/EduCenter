using EduCenterSrv.DataBase;

using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public class UserSrv
    {
        private EduDbContext _dbContext;
        public UserSrv(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
