using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test_OP_Web.Data;
using Test_OP_Web.Data.Options;
using Test_OP_Web.Logging;
using Test_OP_Web.Services;

namespace Test_OP_Web
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
            services.AddControllersWithViews();


            services.AddTransient<IStatisticsService, StatisticsService>();

            var def = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<OptionContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<UserAxe, IdentityRole>()
                .AddEntityFrameworkStores<OptionContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<ILogger, Logger>();
            services.AddTransient<VkNetMethods>();
            services.AddTransient<StatisticsService>();


            services.AddDatabaseDeveloperPageExceptionFilter();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Session}/{action=Index}/{id?}");
            });
        }
    }
}
