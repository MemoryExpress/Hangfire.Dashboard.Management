using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire.Dashboard.Management;

namespace Hangfire.Dashboard.Management.Standard.Test
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddHangfire(x =>
            {
                x
                    .UseColouredConsoleLogProvider()//ʹ�ò�ɫ����̨��־�ṩ����
                    //.UseLog4NetLogProvider()//ʹ��log4net��־�ṩ����
                    //.UseNLogLogProvider()//ʹ��NLogLog��־�ṩ����
                    //.UseConsole()//ʹ�ÿ���̨����(Hangfire.Console)

                    //.UseActivator(new OrchardJobActivator(_lifetimeScope))
                    //.UseFilter(new LogFailureAttribute())//��¼ʧ����־��¼

                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.ServerCount)//����������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.RecurringJobCount)//��������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.RetriesCount)//���Դ���
                    //.UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.EnqueuedCountOrNull)//��������
                    //.UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.FailedCountOrNull)//ʧ������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.EnqueuedAndQueueCount)//��������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.ScheduledCount)//�ƻ���������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.ProcessingCount)//ִ���е���������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.SucceededCount)//�ɹ���ҵ����
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.FailedCount)//ʧ������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.DeletedCount)//ɾ������
                    .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.AwaitingCount)//�ȴ���������
                ;
                //x
                //    .UseDashboardMetric(Hangfire.SqlServer.SqlServerStorage.ActiveConnections)//���������
                //    .UseDashboardMetric(Hangfire.SqlServer.SqlServerStorage.TotalConnections)//����������
                //    .UseSqlServerStorage("server=10.11.1.11;database=Hangfire;uid=sa;pwd=`1q2w3e4r;Application Name=WebErpApp (Hangfire) Data Provider", new Hangfire.SqlServer.SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) })
                //;
                x.UseManagementPages((cc) => cc.AddJobs(GetModuleTypes()))
                    .UseMemoryStorage()
                    ;
            });
        }
        public static Type[] GetModuleTypes()
        {
            //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //var moduleDirectory = System.IO.Path.Combine(baseDirectory, "Modules");
            //var assembliePaths = System.IO.Directory.GetFiles(baseDirectory, "*.dll");
            //if (System.IO.Directory.Exists(moduleDirectory))
            //    assembliePaths = assembliePaths.Concat(System.IO.Directory.GetFiles(moduleDirectory, "*.dll")).ToArray();

            //var assemblies = assembliePaths.Select(f => System.Reflection.Assembly.LoadFile(f)).ToArray();
            var assemblies = new[] { typeof(Startup).Assembly };
            var moduleTypes = assemblies.SelectMany(f =>
            {
                try
                {
                    return f.GetTypes();
                }
                catch (Exception)
                {

                    return new Type[] { };
                }
            }


            )/*.Where(f => f.IsClass && typeof(IClientModule).IsAssignableFrom(f))*/.ToArray();

            return moduleTypes;
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();

            app.UseHangfireServer();//����Hangfire����
            app.UseHangfireDashboard();//����hangfire���
        }
    }
}
