using EduCenterModel.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduCenterWeb.DataBase
{
    public class EduDbContext: DbContext
    {
        public EduDbContext() { }

        public EduDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
         //   optionsBuilder.UseSqlServer(Configuration.GetConnectionString("EduCenterDB")))
        }
        public DbSet<EUserInfo> DBUserInfo { get; set; }

        public DbSet<EUserInfoBackEnd> DBUserInfoBackEnd { get; set; }
    }
}
