using JobSityChat.Infraestructure.Repositories;
using JobSityChat.Web.Infraestructure.StartupFilters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSityChat.Web.Infraestructure.Extensions
{
    public static class DBContextExtension
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services,
               IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(
                       configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<IStartupFilter, MigrationStartupFilter<ApplicationDbContext>>();
            return services;
        }
    }
}
