using Ardalis.GuardClauses;
using DataAccessService;
using DddInfrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebAPI
{
    public class Startup
    {
        const string apiVer = "v1";
        const string apiName = "PhoneBook API";

        public Startup(IWebHostEnvironment env)
        {
            Guard.Against.Null(env, nameof(env));

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Environment
                    .GetCommandLineArgs()
                    .Skip(1)
                    .ToArray()
                );

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Not compatible with ASP.NET MVC convention over configuration principle")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<PhoneBookDbContext>(options => options.UseSqlServer(connectionString));

            services
                .AddControllersWithViews()
                .AddJsonOptions(opts => { opts.JsonSerializerOptions.IgnoreNullValues = true; })
                .AddNewtonsoftJson();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(apiVer, new OpenApiInfo { Title = apiName, Version = apiVer });
            });
        }


        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Not compatible with ASP.NET MVC convention over configuration principle")]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IHostApplicationLifetime lifetime)
        {
            Guard.Against.Null(app, nameof(app));
            Guard.Against.Null(lifetime, nameof(lifetime));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{apiVer}/swagger.json", apiName);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "angular_fallback",
                    pattern: "{target:regex(phonebook):nonfile}/{*catchall}",
                    defaults: new { controller = "Home", action = "Index" }
                );
            });

            app.UseSpa(spa =>
            {
                var strategy = Configuration.GetValue<string>("DevTools:ConnectionStrategy");
                if (strategy == "proxy")
                {
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
                else
                {
                    spa.Options.SourcePath = "../AngularUI";
                    spa.UseAngularCliServer("start");
                }
            });

            SeedData.SeedDatabase(services.GetRequiredService<PhoneBookDbContext>());
        }
    }
}
