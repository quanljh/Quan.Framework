/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Framework
* FileName      :  FrameworkConstruction
* CreateTime    :  2020/05/06 20:54:19
************************************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quan
{
    /// <summary>
    /// The construction information when starting up and configuration Quan.Framework
    /// </summary>
    public class FrameworkConstruction
    {
        #region Public Properties

        /// <summary>
        /// The services that will get used and compiled once the framework is built
        /// </summary>
        public IServiceCollection Services { get; set; }

        /// <summary>
        /// The environment used for the Quan.Framework
        /// </summary>
        public FrameworkEnvironment Environment { get; set; }


        /// <summary>
        /// The configuration used for the Quan.Framework
        /// </summary>
        public IConfiguration Configuration { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FrameworkConstruction()
        {
            // Create a new list of dependencies
            Services = new ServiceCollection();

            // Create environment details
            Environment = new FrameworkEnvironment();

            // Inject environment into services
            Services.AddSingleton(Environment);
        }

        #endregion
    }
}
