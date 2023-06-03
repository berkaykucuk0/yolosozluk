using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YoloSozluk.Api.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.Development.json")
           .Build();


            //TODO burasý düzenlenecek

            var sinkOpts = new MSSqlServerSinkOptions();
            sinkOpts.TableName = configuration.GetSection("Serilog:TableName").Value;
            sinkOpts.SchemaName = configuration.GetSection("Serilog:SchemaName").Value;
            sinkOpts.AutoCreateSqlTable = true;

            var columnOpts = new ColumnOptions();
            columnOpts.Store.Remove(StandardColumn.MessageTemplate);
            columnOpts.Store.Remove(StandardColumn.Properties);
            columnOpts.Store.Add(StandardColumn.LogEvent);
            columnOpts.LogEvent.DataLength = 2048;
            columnOpts.TimeStamp.NonClusteredIndex = true;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
                    appConfiguration: configuration,
                    sinkOptions : sinkOpts,
                    columnOptions: columnOpts
                    )
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
