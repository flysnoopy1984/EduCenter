using EduCenterCore.Common.Helper;
using EduCenterSrv;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;

namespace EduCenterConsole
{
    class Program
    {

        private static readonly IConfigurationBuilder Configuration = new ConfigurationBuilder();
        private static IConfigurationRoot _configuration;
        private static EduDbContext _dbContext;//数据库访问的DBContext
        private static ConsoleSrv _ConsoleSrv;
        

        private void TestTime()
        {
            var date = DateTime.Now;
            Thread.Sleep(2000);
            Console.WriteLine(DateSrv.DateTimeForClient(date));
            Console.Read();
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            try
            {
                Init();
                InitGlobalData();
                NLogHelper.ConsoleInfo($"初始化完成.");
                _ConsoleSrv = new ConsoleSrv(_dbContext);
                _ConsoleSrv.RunJob_FixUserCourse();
                NLogHelper.ConsoleInfo($"[修复完成]");

                NLogHelper.ConsoleInfo($"同步公众号文章.");
                SyncWXNews syncWXNews = new SyncWXNews(_dbContext);
                syncWXNews.Run();

                NLogHelper.ConsoleInfo($"同步了{syncWXNews.SyncCount}条文章，已完成！");
            

            }
            catch(Exception ex)
            {
                NLogHelper.ConsoleError($"出错啦! {ex.Message}");
         
            }
           

        }

        private static void InitGlobalData()
        {
            StaticDataSrv.InitDbData(_dbContext);
        }

        private static void Init()
        {
            dynamic type = (new Program()).GetType();
            string currentDirectory = Path.GetDirectoryName(type.Assembly.Location);

            NLogHelper.ConsoleInfo($"RunPath:{currentDirectory}");
            _configuration = Configuration.SetBasePath(currentDirectory)
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .Build();


            var serviceCollection = new ServiceCollection()
                .AddDbContextPool<EduDbContext>(options =>
                {
                    options.UseSqlServer(_configuration.GetConnectionString("EduCenterDB"), //读取配置文件中的链接字符串
                    b => b.UseRowNumberForPaging());
                })
                .AddTransient<EduDbContext>()
                .AddOptions();
            var provider = serviceCollection.BuildServiceProvider();
            _dbContext = provider.GetService<EduDbContext>();

        }
    }
}
