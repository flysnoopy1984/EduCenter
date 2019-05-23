using EduCenterModel.BaseEnum;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
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

        /// <summary>
        /// 主数据调用
        /// </summary>
        /// <returns></returns>
        public List<ECourseInfo> GetAllList()
        {
           return  _dbContext.Set<ECourseInfo>().ToList();
        
        }

        /// <summary>
        /// 老师技能列表
        /// </summary>
        /// <returns></returns>
        public List<SCourse> GetSimpleList()
        {
            return _dbContext.DBCourseInfo.Select(a => new SCourse
            {
                Code = a.Code,
                Name = a.TypeName,
                RecordStatus = a.RecordStatus,

            }).Where(a=>a.RecordStatus == RecordStatus.Normal).ToList();
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
