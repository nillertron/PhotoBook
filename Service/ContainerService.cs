using DataAcces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace Service
{
    public static class ContainerService
    {
        public static IServiceCollection AddServices(IServiceCollection services)
        {
            var serviceList = Assembly.Load(nameof(Service)).GetTypes().Where(x => !x.IsNested && !x.Name.StartsWith("I") && !x.Name.Contains("ContainerService") && !x.Name.Contains("Resources")).ToList();
            foreach (var e in serviceList)
            {
                if(e.Name == "LoginStateService")
                {
                    services.AddScoped(e.GetInterface("I" + e.Name.ToString()), e);
                }
                else if(e.Name == "RestClient")
                {
                    services.AddScoped(e.GetInterface("I" + e.Name.ToString()), e);
                }
                else
                {
                    services.AddTransient(e.GetInterface("I" + e.Name.ToString()), e);
                }
            }
            return services;
        }
        public static IServiceCollection AddModels(IServiceCollection services)
        {

            var modelList = Assembly.Load(nameof(Model)).GetTypes().Where(x => !x.IsNested && !x.Name.Contains("BaseEntity")).ToList();
            foreach (var e in modelList)
            {
                services.AddTransient(e);
            }
            return services;
        }
        public static IServiceCollection AddDataAcces(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddDbContext<Context>(o => o.UseMySQL(Properties.Resources.DefaultConnection));

            var repoList = Assembly.Load(nameof(DataAcces)).GetTypes().ToList();
            repoList = repoList.Where(x => x.FullName.Contains("Specific") && !x.IsNested).ToList();
            foreach (var e in repoList)
            {
                services.AddTransient(e);
            }
            return services;

        }

        
    }
}
