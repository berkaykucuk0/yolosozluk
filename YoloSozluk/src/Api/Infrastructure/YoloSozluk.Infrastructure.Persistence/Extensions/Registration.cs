using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Infrastructure;
using YoloSozluk.Infrastructure.Persistence.Context;
using YoloSozluk.Infrastructure.Persistence.Repositories;

namespace YoloSozluk.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration conf)
        {
            var cnnStr = conf["YoloConnection"];
            services.AddDbContext<YoloSozlukContext>(conf =>
            {
                conf.UseSqlServer(cnnStr);
            });

            //Data seeding yapmak için burayı aç
            //var seedData = new SeedData();
            //seedData.SeedAsync(conf).GetAwaiter().GetResult();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<TokenGenerator>();


            return services;
        }
    }
}
