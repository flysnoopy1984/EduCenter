using CMSSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSSrv.Srv
{
    public class baseCMSSrv
    {
        protected CmsDbContext _dbContext;
        public CmsDbContext CmsDbContext
        {
            get { return _dbContext; }
        }
        public baseCMSSrv(CmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
