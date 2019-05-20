using EduCenterModel.Teacher;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public class TecSrv : BaseSrvMasterData<ETecInfo>
    {
        public TecSrv(EduDbContext dbContext):base(dbContext)
        {

        }
    }
}
