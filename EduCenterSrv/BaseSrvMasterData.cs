using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public class BaseSrvMasterData<T>:BaseSrv where T:class
    {
       
        public BaseSrvMasterData(EduDbContext dbContext) : base(dbContext)
        {
           
        }

        public void Add(T newObj,bool saveNow = true)
        {
            _dbContext.Add<T>(newObj);
            if(saveNow)
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
