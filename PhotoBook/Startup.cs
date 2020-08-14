using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Service;
namespace PhotoBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o => o.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddRazorPages();
            //services.AddDbContext<Context>(o => o.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddServerSideBlazor();
            services = ContainerService.AddDataAcces(services);
            services = ContainerService.AddServices(services);
            services = ContainerService.AddModels(services);
            services = AddWebAPI(services);
            services.AddCors(o => {
            o.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            });



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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMvcWithDefaultRoute();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
        private IServiceCollection AddWebAPI(IServiceCollection services)
        {
            var apiList = Assembly.Load(nameof(PhotoBook)).GetTypes().Where(x => x.Namespace.Contains("API") && !x.IsNested).ToList();
            foreach (var e in apiList)
            {
                services.AddTransient(e);
            }
            return services;
        }
    }
}
