using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.AppFramework;
using EduCenterCore.EduFramework;
using EduCenterSrv;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EduAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddJsonOptions(opt =>
               {
                   /*DefaultContractResolver 是原样输出，后台属性怎么写的，返回的 json 就是怎样的。
                     CamelCasePropertyNamesContractResolver ：驼峰命名法，首字母小写。如果变量全为大写，比如：NAME，返回的是 name */
                   opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
               });
               

            services.AddDbContext<EduDbContext>(
              op => op.UseSqlServer(Configuration.GetConnectionString("EduCenterDB"),
              c => c.MigrationsAssembly("EduCenterWeb")
              ));

            services.AddScoped<CourseSrv>();
            services.AddScoped<TecSrv>();
            services.AddScoped<UserSrv>();
            services.AddScoped<WxMiniSrv>();
            services.AddScoped<ResSrv>();
            services.AddScoped<AppEduSrv>();

            services.AddMemoryCache();
        }

        public static void InitGlobalData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
            {
                EduDbContext context = serviceScope.ServiceProvider.GetService<EduDbContext>();
                StaticDataSrv.InitDbData(context);

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            EduConfigReader.SetConfiguration(Configuration);
            XYAppConfigReader.SetConfiguration(Configuration);

            EduEnviroment.SetEnviroment(env);

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc();

            StaticDataSrv.Init();
            InitGlobalData(app);
        }
    }
}
