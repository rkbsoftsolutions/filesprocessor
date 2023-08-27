using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReferigenatorSvc.dbcontext;
using ReferigenatorSvc.Filters;
using ReferigenatorSvc.Hub.NotificationHub;
using ReferigenatorSvc.Models;
using ReferigenatorSvc.Services;
using Microsoft.AspNetCore.SignalR;
namespace ReferigenatorSvc
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
            var cs = Configuration.GetValue<string>("ConnectionString");
            var dbPath = Path.Combine(Environment.CurrentDirectory, cs);
            services.AddControllersWithViews();
            services.RegisterMapsterConfiguration();
            services.AddSignalR(hubOptions =>
            {
                hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(30);
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(15);
            });
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlite("Filename = " + dbPath);
        //        // don't raise the error warning us that the in memory db doesn't support transactions
        //.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReferigenatorService, ReferigenatorService>();
            services.AddScoped(typeof(TransactionRequiredAttribute));
            services.Configure<List<StorageTypes>>(Configuration.GetSection("StorageTypes"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("notify", x =>
                {
                    x.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
                });
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
