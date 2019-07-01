using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduCenterModel.User;

namespace EduCenterSrv
{
    public class BackendSrv: BaseSrv
    {
        public BackendSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        public EUserInfoBackEnd UserLogin(string loginName,string loginPwd)
        {
            EUserInfoBackEnd result = _dbContext.DBUserInfoBackEnd.Where(a => a.LoginName == loginName && a.LoginPwd == loginPwd).FirstOrDefault();
            return result;
           
        }
    }
}
