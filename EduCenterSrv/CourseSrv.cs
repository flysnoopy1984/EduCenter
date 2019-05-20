using EduCenterModel.Course;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduCenterSrv
{
    public class CourseSrv: BaseSrvMasterData<ECourseInfo>
    {
      //  private EduDbContext _dbContext;
        public CourseSrv(EduDbContext dbContext):base(dbContext)
        {
            
        }

        public List<ECourseInfo> GetAllList()
        {
           return  _dbContext.Set<ECourseInfo>().ToList();
         //  return _dbContext.DBCourseInfo.ToList();
        }

        public ECourseInfo Get(string pk)
        {
            return _dbContext.DBCourseInfo.Where<ECourseInfo>(a => a.Code == pk).FirstOrDefault();
        }

        //public void Add(ECourseInfo newObj)
        //{
        //    _dbContext.Add<ECourseInfo>(newObj);
        //    _dbContext.SaveChanges();
        //}

        //public void Update(ECourseInfo Obj)
        //{
        //    _dbContext.Update<ECourseInfo>(Obj);
        //    _dbContext.SaveChanges();
        //}

      

        public void Delete(string Code)
        {
            ECourseInfo delObj = new ECourseInfo
            {
                Code = Code,
            };

            this.Delete(delObj);
            
        }
    }
}
