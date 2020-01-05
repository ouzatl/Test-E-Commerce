using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Swashbuckle.AspNetCore.Swagger;
using Test.ECommerce.Common.Config;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data;
using Test.ECommerce.Data.Mapping;
using Test.ECommerce.Data.Repositories.ProductRepository;
using Test.ECommerce.Service.ProductService;

namespace Test.ECommerce.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //Read appsetting.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //NLog
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Read appsetting.json
            services.AddOptions();
            services.Configure<SiteConfig>(Configuration.GetSection("SiteConfig"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //In-Memory Cache
            services.AddMemoryCache();

            //ConnectionString
            services.AddDbContext<TestECommerceContext>(options => options.UseSqlServer(Configuration["dbConnection"]));

            //Swagger API UI
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Core API", Description = "Test E Commerce API" });

                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"TestECommerce.API.xml";

                x.IncludeXmlComments(xmlPath);
            }
            );

            //NLog
            services.AddSingleton<ILog, LogNLog>();

            //Dependency Service
            services.AddScoped<IProductService, ProductService>();

            //Dependency Repository
            services.AddScoped<IProductRepository, ProductRepository>();

            //Mapping
            services.AddAutoMapper(x => x.AddProfile(new Mapping()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Swagger API UI
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Test E Commerce API");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}