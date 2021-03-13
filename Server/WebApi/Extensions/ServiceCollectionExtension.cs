using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Model.AutoMapper;
using Repository.Connection;
using Repository.Repositories;
using Service.Interfaces.IFile;
using Service.Interfaces.ILineItem;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddTransientServices(this IServiceCollection services)
        {
            #region Service Transient
            //services.AddTransient<ILoginService, UsersService>();
            services.AddTransient<ILineItemFileService, FileService>();
            services.AddTransient<IReportService, LineItemService>();
            services.AddTransient<IRestoredLineItemService, LineItemService>();
            #endregion


            #region Repository Transient
            services.AddTransient<LineItemRepository>();

            #endregion

        }

        /// <summary>
        /// Scoped
        /// </summary>
        /// <param name="services"></param>
        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<SqlServerConnection>();
            services.AddScoped<IDbConnection, SqlConnection>();

        }

        /// <summary>
        /// Singleton
        /// </summary>
        /// <param name="services"></param>
        public static void AddSingletonServices(this IServiceCollection services)
        {

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());
        }
    }
}
