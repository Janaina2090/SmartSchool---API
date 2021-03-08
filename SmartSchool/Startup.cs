using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartSchool
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
            services.AddDbContext<SmartContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SmartDB")));
            services.AddScoped<IRepository, Repository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("smartschoolapi",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title= "SmartSchool API",
                        Version= "1.0"
                    });
                var xmlComentario = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlComentarioCaminho = Path.Combine(AppContext.BaseDirectory, xmlComentario);

                options.IncludeXmlComments(xmlComentarioCaminho);
            });

         

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
               options.SwaggerEndpoint("swagger/smartschoolapi/swagger.json", "smartschoolapi");
               options.RoutePrefix = "";
                });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
