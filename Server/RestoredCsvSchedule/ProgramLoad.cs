using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Configuration;
using Repository.Connection;
using Repository.Repositories;
using RestoredCsvSchedule.Configuration;
using Service.Interfaces.IFile;
using Service.Interfaces.ILineItem;
using Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RestoredCsvSchedule
{
    public class ProgramLoad
    {
        public static IHost _host;

        /// <summary>
        /// Service DI
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void CreateHostBuilder(string[] args)
        {
            _host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                            services.AddScoped<System.Data.IDbConnection, SqlConnection>()
                                    .AddScoped<SqlServerConnection>()
                                    .AddMemoryCache()
                                    .AddTransient<LineItemRepository>()
                                    .AddTransient<ILineItemFileService, FileService>()
                                    .AddTransient<IRestoredLineItemService, LineItemService>()
                                    ).Build();

        }


        /// <summary>
        /// Load AppSettings
        /// </summary>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return _host.Services.GetRequiredService<T>();
        }

        /// <summary>
        /// Load AppSettings
        /// </summary>
        /// <returns></returns>
        public static void LoadConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configuration/RestoredCsvScheduleSettings.json", false)
                .AddJsonFile("Configuration/ConnectionStrings.json", false)
                .Build();

            configurationBuilder.Bind(new RestoredCsvScheduleSettings());
            configurationBuilder.Bind(new ConnectionStrings());
        }
    }
}
