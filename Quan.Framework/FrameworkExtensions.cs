/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Dna
* FileName      :  FrameworkExtensions
* CreateTime    :  2020/05/05 10:50:37
************************************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Quan
{
    /// <summary>
    /// Extension methods for the Dna framework
    /// </summary>
    public static class FrameworkExtensions
    {
        /// <summary>
        /// Adds a default logger so that we can get a non-generic ILogger
        /// that will have the category name of "Dna"
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultLogger(this IServiceCollection services)
        {
            // Add a default logger
            services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Dna"));

            // Return the services
            return services;
        }
    }
}
