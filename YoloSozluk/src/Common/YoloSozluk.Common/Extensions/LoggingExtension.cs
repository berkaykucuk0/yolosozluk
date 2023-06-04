using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;

namespace YoloSozluk.Common
{
    public static class LoggingExtension
    {
        public static void ConfigureLogging()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

            var sinkOpts = new MSSqlServerSinkOptions();
            sinkOpts.TableName = configuration.GetSection("Serilog:TableName").Value;
            sinkOpts.SchemaName = configuration.GetSection("Serilog:SchemaName").Value;
            sinkOpts.AutoCreateSqlTable = true;

           
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
                    appConfiguration: configuration,
                    sinkOptions: sinkOpts,
                    columnOptions: GetColumnOptions(),
                    restrictedToMinimumLevel:LogEventLevel.Warning
                    )
                .CreateLogger();
        }

        public static ColumnOptions GetColumnOptions()
        {
            var columnOpts = new ColumnOptions();
            columnOpts.Store.Remove(StandardColumn.MessageTemplate);
            columnOpts.Store.Remove(StandardColumn.Properties);

            columnOpts.AdditionalColumns = new List<SqlColumn>
            {
                new SqlColumn
                {
                    DataType =  SqlDbType.VarChar,
                    ColumnName = "Method",
                    AllowNull = true
                },
                new SqlColumn
                {
                    DataType =  SqlDbType.VarChar,
                    ColumnName = "Request",
                    AllowNull = true
                },
                 new SqlColumn
                {
                    DataType =  SqlDbType.VarChar,
                    ColumnName = "Response",
                    AllowNull = true
                }
            };

            return columnOpts;
        }

        public static void YoloErrorLog(Exception ex, string methodName, object request, string response = null)
        {
            using (LogContext.PushProperty("Method", methodName))
            using (LogContext.PushProperty("Request", JsonSerializer.Serialize(request)))
            using (LogContext.PushProperty("Response", string.Empty))
            {
                Log.Error(ex, ex.Message);
            }
        }

        public static void YoloInformationLog( string methodName, object request, object response)
        {
            using (LogContext.PushProperty("Method", methodName))
            using (LogContext.PushProperty("Request", JsonSerializer.Serialize(request)))
            using (LogContext.PushProperty("Response", JsonSerializer.Serialize(response)))
            {
                Log.Information("Operation completed successfully");
            }
        }
    }
}
