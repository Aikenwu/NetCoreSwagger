using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace NetCoreSwaggerDemo
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
            services.AddSwaggerGen(c=>{
                c.SwaggerDoc("v1",new Info{
                    Title="Aikenwu API",Version="V1"
                    ,Description="A simple api made by Aikenwu"
                    ,TermsOfService="None"
                    ,Contact=new Contact(){
                        Name="Aikenwu",
                        Email="Aikenwu@qq.com",
                        Url="https://github.com/Aikenwu"
                    },
                    License=new License{
                        Name="MIT License",
                        Url="https://github.com/Aikenwu"
                    }
                    });
                var basePath = Path.GetDirectoryName(AppContext.BaseDirectory);
                var xmlPath = Path.Combine(basePath, "NetCoreSwaggerDemo.xml");
                c.IncludeXmlComments(xmlPath);

            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c=>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Aikenwu API V1");
                //c.RoutePrefix=string.Empty;
            });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
