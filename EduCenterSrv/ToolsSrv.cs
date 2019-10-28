using EduCenterModel.Tools;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduCenterSrv
{
    public class ToolsSrv:BaseSrv
    {
        public ToolsSrv(EduDbContext dbContext) : base(dbContext) { }

        public ELessonQR GetQR(string code)
        {
            return _dbContext.DbLessonQR.Where(a => a.Code == code).FirstOrDefault();
        }

        public void AddQR(ELessonQR qR)
        {
            _dbContext.DbLessonQR.Add(qR);
        }
    }
}
