using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationSvc;
using EsearchSvc.Services.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platforms;
using Platforms.MIddleware;
using ReferigenatorSvc.dbcontext;
using ReferigenatorSvc.Models;
using ReferigenatorSvc.Services;

namespace rkbmarketplace
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
            var cs = Configuration.GetValue<string>("ConnectionString:refigendb");
            var dbPath = Path.Combine(Environment.CurrentDirectory, cs);
            var userdb = Configuration.GetValue<string>("ConnectionString:userdb");
            var userDBPath = Path.Combine(Environment.CurrentDirectory, userdb);

            services.AddPlatformAuthentication(Configuration);
            services.AddControllers();

            services.AddDbContext<StoreDbContext>(o => o.UseSqlite("Filename = " + dbPath));
            services.AddDbContext<UserPlatfromdbContext>(o => o.UseSqlite("Filename = " + userDBPath));
            //services.AddScoped(typeof(TransactionRequiredAttribute));
            services.Configure<List<StorageTypes>>(Configuration.GetSection("StorageTypes"));
            services.AddScoped<IRefrigenatorService, RefrigenatorService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UsePlatformAuthentication();
            
        }
    }
}
