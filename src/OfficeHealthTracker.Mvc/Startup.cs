using OfficeHealthTracker.Infrastructure.Data;
using OfficeHealthTracker.Interfaces.Repository;
using OfficeHealthTracker.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeHealthTracker.Application.Services;
using OfficeHealthTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OfficeHealthTracker.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using OfficeHealthTracker.Infrastructure;

namespace WebApplication1E
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            // Add Redis distributed cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _configuration.GetConnectionString("Redis"); // Connection string to your Redis instance
                options.InstanceName = "ConnectionString:"; // Prefix for keys in Redis
            });

            // Add JWT authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]))
                };
            });
            GenerateToken.Initialize(_configuration);
            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IFilledFormRepository, FilledFormRepository>();
            services.AddScoped<IFieldTypeRepository, FieldTypeRepository>();
            services.AddScoped<IPayloadRepository, PayloadRepository>();
            // Register services
            services.AddScoped<IUserService, UserService>();
         
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IFilledFormService, FilledFormService>();
            services.AddScoped<IFieldTypeService, FieldTypeService>();
            services.AddScoped<IPayloadService, PayloadService>();
            services.AddAuthorization();
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); 
            services.AddHttpContextAccessor();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();
            // Add framework services.
            services.AddControllers();
            services.AddLogging(logging =>
            {
                var logFilePath = _configuration.GetValue<string>("Logging:LogFile");
                logging.AddProvider(new FileLoggerProvider(logFilePath));
                logging.SetMinimumLevel(LogLevel.Information);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
