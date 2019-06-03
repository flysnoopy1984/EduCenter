using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public class BaseSrv
    {
        protected EduDbContext _dbContext;
        public BaseSrv(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();

        }

        public void BeginTrans()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTrans()
        {
            _dbContext.Database.CommitTransaction();

        }

        public void RollBackTrans()
        {
            _dbContext.Database.RollbackTransaction();

        }
    }
}
