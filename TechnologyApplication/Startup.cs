using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TechnologyApplication
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


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechnologyApplication", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseMiddleware();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("HelloWorld");

            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("middleware2");
            //    await next();
            //});
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("middleware1");
            //});
            //app.Run(async (context) =>
            //{

            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechnologyApplication v1"));
            }

            //public void Configure(IApplicationBuilder applicationBuilder, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
            //{
            //    app.Run();
            //}
            //app.Run(Middleware);

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("hello c#");
            //}); 
            //app.Use(async (context,next) =>
            //{
            //    await context.Response.WriteAsync("hello backend");
            //    await next();
            //});

            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("home.html");
            //app.UseDefaultFiles(options);
            //app.UseStaticFiles();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("hello api");
            //});

            app.UseHttpsRedirection();

            app.UseRouting();
 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        //private async Task Middleware (HttpContext context)
        //{
        //    await context.Response.WriteAsync("hello  web api");
        //}

        //public void Configaration(IApplicationBuilder app, IHostBuilder host)
        //{
        //    app.Run(Middleware);
        //}
        //private async Task MiddleWare (HttpContext context)
        //{
        //    await context.Response.WriteAsync("middleware");
        //}

    }
}
