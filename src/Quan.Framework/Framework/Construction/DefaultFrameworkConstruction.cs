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
    ///     This is an example setup code for building a Dna Framework Construction
    /// </para>
    /// 
    /// <code>
    /// 
    ///     // Build the framework adding any required services
    ///     Framework.Construction.DefaultFrameworkConstruction()
    ///             .AddFileLogger()
    ///             .Build();
    ///             
    /// </code>
    /// 
    /// </example>
    public class DefaultFrameworkConstruction : FrameworkConstruction
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFrameworkConstruction()
        {
            // Configure...
            this.AddDefaultConfiguration()
                // And add default services
                .AddDefaultServices();
        }

        /// <summary>
        /// Constructor with configuration options
        /// </summary>
        public DefaultFrameworkConstruction(Action<IConfigurationBuilder> configure = null)
        {
            // Configure...
            this.AddDefaultConfiguration(configure)
                // And add default services
                .AddDefaultServices();
        }

        #endregion
    }
}
