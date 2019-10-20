using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GameServer
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

            services.ConfigureApplicationCookie(opts => opts.Cookie.SameSite = SameSiteMode.None);

            services.AddCors(options => options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:8080", "https://rwherber.com")
                        .AllowCredentials();
                }));

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TestHub>("/test");
            });
        }
    }
}
