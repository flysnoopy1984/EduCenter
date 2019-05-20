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
        
        }

        public ECourseInfo Get(string pk)
        {
            return _dbContext.DBCourseInfo.Where<ECourseInfo>(a => a.Code == pk).FirstOrDefault();
        }

      

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
