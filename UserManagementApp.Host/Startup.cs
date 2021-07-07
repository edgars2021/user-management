using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UserManagementApp.Core;
using UserManagementApp.Core.Repositories;
using UserManagementApp.Core.Services;
using UserManagementApp.HttpApi;

namespace UserManagementApp.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ValidateModelAttribute>();

            services
                .AddControllers()
                .AddApplicationPart(typeof(UsersController).Assembly);

            services.AddDbContext<UserManagementContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqLiteDb")), 
                contextLifetime: ServiceLifetime.Transient, 
                optionsLifetime: ServiceLifetime.Singleton);
            
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "UserManagementApi", Version = "v1"});
            });
            
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementApi v1"));
                
                app.UseSpaStaticFiles();
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}