using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Northwind.Bll;
using Northwind.Dal.Abstract;
using Northwind.Dal.Concrete.EntityFramework.Context;
using Northwind.Dal.Concrete.EntityFramework.Repository;
using Northwind.Dal.Concrete.EntityFramework.UnitOfWork;
using Nortwind.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nortwind.WebApi
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
            #region JwtTokenService
            //JwtSecurityTokenHandler 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(cfg =>
            {
                cfg.SaveToken = true;
                cfg.RequireHttpsMetadata = false;
                cfg.TokenValidationParameters = new TokenValidationParameters 
                {
                    RoleClaimType = "Roles",
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Token:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["Token:Audience"],

                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))

                };
            });
            #endregion

            #region ApplicationContext
            //services.AddDbContext<NORTHWNDContext>();
            //services.AddScoped<DbContext, NORTHWNDContext>();
            services.AddScoped<DbContext, NORTHWNDContext>();
            services.AddDbContext<NORTHWNDContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sqlOpt  =>
                {
                    sqlOpt.MigrationsAssembly("Northwind.Dal");
                });
            });


            #endregion

            #region ServiceSection
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICustomerCustomerDemoService, CustomerCustomerDemoManager>();
            services.AddScoped<ICustomerDemographicService, CustomerDemographicManager>();
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<IEmployeeService, EmployeeManager>();
            services.AddScoped<IEmployeeTerritoryService, EmployeeTerritoryManager>();
            services.AddScoped<IOrderDetailService, OrderDetailManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IRegionService, RegionManager>();
            services.AddScoped<IShipperService, ShipperManager>();
            services.AddScoped<ISupplierService, SupplierManager>();
            services.AddScoped<ITerritoryService, TerritoryManager>();
            services.AddScoped<IUserService, UserManager>();
            #endregion

            //T managerda T repository kullanilmadigi(ek metod kullanilmadigi) icin eklenmedi.
            #region RepositorySection   
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nortwind.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nortwind.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
