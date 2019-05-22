using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduCenterSrv
{
    public class TecSrv : BaseSrvMasterData<ETecInfo>
    {
        
        public TecSrv(EduDbContext dbContext):base(dbContext)
        {
           
        }

       

        public List<SiKsV> GetSkillLevelList()
        {
            BaseEnumSrv BaseEnumSrv = new BaseEnumSrv();
            return BaseEnumSrv.GetSkillLevel();
        }

        public List<STec> GetSimpleList()
        {

            return _dbContext.DBTecInfo.Select(a => new STec
            {
                Code = a.TecCode,
                Name = a.Name
            }).ToList();
        }

        public void NewTecFromWX()
        {

        }

        
    }
}
