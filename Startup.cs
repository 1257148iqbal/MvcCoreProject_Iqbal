﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCoreProject_Iqbal.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcCoreProject_Iqbal.Models;

namespace MvcCoreProject_Iqbal
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

            services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //services.AddDefaultIdentity<IdentityUser>().AddDefaultUI(UIFramework.Bootstrap4).AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddAuthorization(options =>
            {

                options.AddPolicy("CreateRolePolicy",
                policy => policy.RequireClaim("Create Role", "true"));



                options.AddPolicy("DeleteRolePolicy",
policy => policy.RequireClaim("Delete Role", "true"));

                options.AddPolicy("EditRolePolicy",
        policy => policy.RequireClaim("Edit Role", "true"));
            });



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddTransient<ITrainerRepository, TrainerRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();

            //services.AddIdentity<IdentityUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
