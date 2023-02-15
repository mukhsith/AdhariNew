using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using System.Threading.Tasks;
using QPay.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Utility.API;

namespace QPay
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped<IStringLocalizer, StringLocalizer<SharedResource>>();
            DependencyRegister.RegisterService(services, Configuration);
            services.AddMvc()
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(o =>
            {
                o.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(SharedResource));
                };
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseRouting();

            app.UseAuthorization();

            var appSettingsModel = Configuration.GetSection("AppSettings").Get<AppSettingsModel>();
            if (appSettingsModel.EnableRedirectToWwwRule)
            {
                var options = new RewriteOptions();
                options.AddRedirectToHttps();
                options.Rules.Add(new RedirectToWwwRule());
                app.UseRewriter(options);
            }

            app.UseEndpoints(endpoints =>
            {
                RouteProvider.RegisterRoutes(endpoints);

                endpoints.MapFallback(context =>
                {
                    context.Response.Redirect("/error");
                    return Task.CompletedTask;
                });
            });
        }
    }
}
