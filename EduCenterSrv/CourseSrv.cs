using EduCenterModel.Course;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduCenterSrv
{
    public class CourseSrv
    {
        private EduDbContext _dbContext;
        public CourseSrv(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ECourseInfo> GetAllList()
        {
           return _dbContext.DBCourseInfo.ToList();
        }

        public void Add(ECourseInfo newObj)
        {
            _dbContext.Add(newObj);
            _dbContext.SaveChanges();
        }
    }
}
