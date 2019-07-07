using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.EduFramework;
using EduCenterSrv;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduCenterWeb
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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddCors(op =>
            {
                op.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddSession(o=> 
            {
                o.IdleTimeout = TimeSpan.FromHours(1);
            });



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddJsonOptions(opt => {
                     /*DefaultContractResolver 是原样输出，后台属性怎么写的，返回的 json 就是怎样的。
                       CamelCasePropertyNamesContractResolver ：驼峰命名法，首字母小写。如果变量全为大写，比如：NAME，返回的是 name */
                     opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                     })
                .AddRazorPagesOptions(options =>
                     {
                         options.RootDirectory = "/Pages";//默认目录
                                                          //options.Conventions.AddPageRoute("/pages", "/1");//重写URL
                    options.Conventions.AddPageRoute("/User/Login", "");//默认主页
                      //  options.Conventions.AddPageRoute("/WebBackend/Login", "");
                     }); ;

           
            services.AddDbContext<EduDbContext>(
                op => op.UseSqlServer(Configuration.GetConnectionString("EduCenterDB"), 
                c => c.MigrationsAssembly("EduCenterWeb")
                ));

            services.AddScoped<CourseSrv>();
            services.AddScoped<TecSrv>();
            services.AddScoped<UserSrv>();
            services.AddScoped<OrderSrv>();
            services.AddScoped<BusinessSrv>();
            services.AddScoped<BackendSrv>();
            services.AddScoped<EduCenterSrv.SMS.SMSSrv>();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

        //    app.UseCors("any");


            EduConfigReader.SetConfiguration(Configuration);
            EduEnviroment.SetEnviroment(env);

            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Add("/WebBackEnd/Login");    
            //app.UseDefaultFiles(options);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();
            app.UseMvc();

            InitGlobalData(app);
            StaticDataSrv.Init();


        

        }
    }
}
