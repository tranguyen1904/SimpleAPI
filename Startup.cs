using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore.SqlServer;
using TestAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TestAPI.Repositories;
using TestAPI.Contracts;
using NLog;
using System.IO;
using TestAPI.Filters;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace TestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }
        
        //public ILifetimeScope AutofacContainer { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
           
            //services.AddDbContext<TestAPIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Server")));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            //services.AddSingleton<ILoggerManager, LoggerManager>();

            //services.AddScoped<ValidationFilterAttribute>();
            
            //services.AddScoped(typeof(ValidateEntityExistsAttribute<>));
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModule>();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<TestAPIContext>()
                    .UseSqlServer(Configuration.GetConnectionString("Server"));
            builder.RegisterType<TestAPIContext>().WithParameter("options", dbContextOptionsBuilder.Options).InstancePerLifetimeScope();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // use IApplicationBuilder.ApplicationServices here to resolve things from the container
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
