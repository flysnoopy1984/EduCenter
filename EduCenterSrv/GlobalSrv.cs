using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduCenterModel.Common;

namespace EduCenterSrv
{
    public class GlobalSrv:BaseSrv
    {
        public GlobalSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        public List<ECourseDateRange> GetCourseDateRangeList()
        {
            return _dbContext.DbCourseDateRange.ToList();
        }

        public List<EHoliday> GetHolidayJson()
        {
            return _dbContext.DBHoliday.ToList();
        }
    }
}
