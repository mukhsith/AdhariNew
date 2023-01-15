using API.Extensions;
using Data.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.GlobalErrorHandling;
using Utility.LoggerService;

namespace Adhari
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;  
            //added comment
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //STEP-1 Activate Logger Service for info + ERROR log
            services.AddSingleton<ILoggerManager, LoggerManager>();


            //DB Context Entity framework
            var conString = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(conString));

            // add all classes scoped in ServiceExtensions
            ServiceExtensions.RegisterService(services, Configuration);

            //for backend
            BackendServiceExtensions.RegisterService(services, Configuration);

            //for frontend
            FrontendServiceExtensions.RegisterService(services, Configuration);

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsModel>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettingsModel>();

            ServiceExtensions.AddAuthentication(services, appSettings);
            ServiceExtensions.AddAuthorization(services);
            ServiceExtensions.AddSwaggerService(services);

            services.AddSingleton<IConfiguration>(Configuration);

            ServiceExtensions.AddCors(services, appSettings);

            //Consider using ReferenceHandler.Preserve on JsonSerializerOptions to support cycles
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //configure auto-mapper
            ServiceExtensions.AddMapper(services);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger, ILoggerFactory loggerFactory)
        { 
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

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
            //to hide api move below code inside env.IsDevelopment()
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Adhari API v1"));

            app.UseHttpsRedirection();

            //image creation folder
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(path, @"Images")),
                RequestPath = new PathString("/Images")
            });

            //pdf creation folder
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(path, @"Pdfs")),
                RequestPath = new PathString("/Pdfs")
            }); 
            ////email template folder
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(path, @"EmailTemplates")),
            //    RequestPath = new PathString("/EmailTemplates")
            //});

            app.UseRouting();

            ServiceExtensions.UseCors(app, env, logger);

            app.ConfigureExceptionHandler(logger);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
