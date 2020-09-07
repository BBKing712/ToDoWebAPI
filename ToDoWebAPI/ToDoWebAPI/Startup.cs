using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToDoWebAPI.Modelle;

namespace ToDoWebAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //see https://docs.microsoft.com/de-de/aspnet/core/security/cors?view=aspnetcore-3.1
            //Vue-Default-Url "http://localhost:8080/"
            //services.AddCors(options =>
            //{

            //    services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //                .WithOrigins("http://localhost:8080/")
            //               .AllowAnyMethod()
            //               .AllowAnyHeader();
            //    }));

            //options.AddPolicy(name: "MyPolicy",
            //    builder =>
            //    {

            //        builder.WithOrigins("http://localhost:8080/")
            //                .WithMethods("PUT", "POST", "DELETE", "GET")
            //                .AllowAnyHeader();
            //    });
            //});
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:8080",
                                        "http://www.contoso.com");
                });
            });

            services.AddDbContext<TodoContext>(opt =>
               opt.UseInMemoryDatabase("TodoList"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors("MyPolicy");
            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
