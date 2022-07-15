using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Infrastructure.Persistence.Context;

namespace YoloSozluk.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration conf)
        {
            var cnnStr = conf.GetConnectionString("YoloConnection");
            services.AddDbContext<YoloSozlukContext>(conf =>
            {
                conf.UseSqlServer(cnnStr);
            });

            //Data seeding yapmak için burayı aç
             //var seedData = new SeedData();
             //seedData.SeedAsync(conf).GetAwaiter().GetResult();

            return services;
        }
    }
}
