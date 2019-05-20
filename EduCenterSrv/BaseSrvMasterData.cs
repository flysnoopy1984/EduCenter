using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public class BaseSrvMasterData<T> where T:class
    {
        protected EduDbContext _dbContext;

        public BaseSrvMasterData(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T newObj)
        {
            _dbContext.Add<T>(newObj);
            _dbContext.SaveChanges();
        }

        public void Update(T Obj)
        {
            _dbContext.Update<T>(Obj);
            _dbContext.SaveChanges();
        }

        protected void Delete(T delObj)
        {
            if (CanDelete(delObj))
            {
               
                _dbContext.Remove(delObj);
                _dbContext.SaveChanges();
            }
        }

        protected virtual bool CanDelete(T delObj)
        {
            return true;
        }

        
    }
}
