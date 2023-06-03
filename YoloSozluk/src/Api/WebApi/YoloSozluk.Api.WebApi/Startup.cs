using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.Extensions;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.Extensions;
using YoloSozluk.Api.WebApi.Extensions;
using TechBuddy.Middlewares.ExceptionHandling;
using System.Net;
using YoloSozluk.Common.Exceptions.User;
using Serilog;

namespace YoloSozluk.Api.WebApi
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

            //Service Registrations
            services.AddInfrastructureRegistration(Configuration);
            services.AddApplicationRegistration();

            services.AddLogging();
        
            services.AddDbContext<YoloSozlukContext>();
            services.AddControllers().AddFluentValidation();

            services.ConfigureAuth(conf: Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YoloSozluk.Api.WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YoloSozluk.Api.WebApi v1"));
            }
          

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.AddTBExceptionHandlingMiddleware(opt =>
            {
                opt.IsDevelopment = env.IsDevelopment();
                opt.DefaultHttpStatusCode = HttpStatusCode.InternalServerError;
                opt.ContentType = "application/json";
                opt.DefaultMessage = "An Unexpected Error Occured!";
               
                opt.ExceptionTypeList.Add<Exception>();
                opt.ExceptionTypeList.Add<ArgumentNullException>();
                opt.ExceptionTypeList.Add<UserException>();
                opt.ExceptionTypeList.Add<EntryException>();
                opt.ExceptionTypeList.Add<UserMailConfirmationException>();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
