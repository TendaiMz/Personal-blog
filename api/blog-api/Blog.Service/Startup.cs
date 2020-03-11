using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blog.Service.Domain.Repository;
using Blog.Service.Domain.Validation;
using Blog.Service.Exceptions;
using Blog.Service.Logging;
using Blog.Service.ProblemDetail;
using Blog.Service.Utils.Options;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Blog.Service
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private readonly SwaggerOptions swaggerOptions;
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;
            swaggerOptions= Configuration.GetSection("SwaggerOptions").Get<SwaggerOptions>();
        }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();        
            services.AddSwaggerGen(c=>c.SwaggerDoc(swaggerOptions.SwaggerDocName, new OpenApiInfo
            {
                Title = swaggerOptions.Title,
                Version =swaggerOptions.Version,
                Description = swaggerOptions.Description,              
                Contact = new OpenApiContact
                {
                    Name = swaggerOptions.Contact.Name,
                    Email = swaggerOptions.Contact.Email,                    
                }
            }));

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleException>(ex => new BusinessRuleProblemDetails(ex));
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerOptions.SwaggerEndPoint, swaggerOptions.EndPointName);
            });
            app.UseSerilogRequestLogging();
            app.UseRouting();           
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
