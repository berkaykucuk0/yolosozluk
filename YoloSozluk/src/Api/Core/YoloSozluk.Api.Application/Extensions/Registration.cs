using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Api.Application.Extensions
{
    public static class Registration
    {

        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            var currAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(currAssembly);
            services.AddAutoMapper(currAssembly);
            services.AddValidatorsFromAssembly(currAssembly);

            return services;
        }
    }
}
