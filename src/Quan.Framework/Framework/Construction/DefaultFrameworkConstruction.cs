/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Framework.Default
* FileName      :  DefaultFrameworkConstruction
* CreateTime    :  2020/05/06 22:01:16
************************************************************************************/

using Microsoft.Extensions.Configuration;
using System;

namespace Quan
{
    /// <summary>
    /// Creates a default framework construction containing all
    /// the default configuration and services
    /// </summary>
    /// <example>
    /// 
    /// <para>
    ///     This is the expected setup code for building a Dna Framework Construction
    /// </para>
    /// 
    /// <code>
    ///     // Build the framework adding any required services
    ///     var framework = new DefaultFrameworkConstruction()
    ///             .AddFileLogger()
    ///             .AddAutoUploader()
    ///             .Build();
    ///             
    ///     // Configure services
    ///     // Sets up environment variables based on the calling assembly
    ///     Framework.SetEnvironment();
    ///     
    ///     // Custom service extensions
    ///     framework.UseYourService1(options => options.Something = true );
    ///     framework.UseYourService2();
    /// </code>
    /// 
    /// </example>
    public class DefaultFrameworkConstruction : FrameworkConstruction
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFrameworkConstruction(Action<IConfigurationBuilder> configure = null)
        {
            // Configure...
            this.Configure(configure)
                // And add default services
                .AddDefaultServices();
        }

        #endregion
    }
}
