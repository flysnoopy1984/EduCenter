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
        public List<STec> GetSimpleList()
        {

            return _dbContext.DBTecInfo.Select(a => new STec
            {
                Code = a.TecCode,
                Name = a.Name
            }).ToList();
        }
    }
}
