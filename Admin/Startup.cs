using Admin.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
namespace Admin
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
            //string key = Utility.Helpers.Common.GetRandomNumber();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //start for localized
            services.AddLocalization(options => options.ResourcesPath = "Resources"); 
            services.AddScoped<IStringLocalizer, StringLocalizer<SharedResource>>(); 
            //end for localized
            DependencyRegister.RegisterService(services, Configuration);
            //start for localized
            services.AddMvc()
                       .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                       .AddDataAnnotationsLocalization(o =>
                       {
                           o.DataAnnotationLocalizerProvider = (type, factory) =>
                           {
                               return factory.Create(typeof(SharedResource));
                           };
                       });

            //end for localized

            //need more info
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            ////start for localized
            var cultures = new List<CultureInfo> {
                    new CultureInfo("ar"),
                    new CultureInfo("en")
                };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
            //end for localized
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

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
            // app.UseRouting();
            
            ////start for localized, if you forget, 2 language will show up in view 
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            //end for localized
            
            //app.UseCors("AllowAnyOrigin");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                RouteProvider.RegisterRoutes(endpoints);

            });
        }
    }
}
