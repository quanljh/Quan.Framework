/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Framework.Default
* FileName      :  DefaultFrameworkConstruction
* CreateTime    :  2020/05/06 22:01:16
************************************************************************************/

namespace Quan
{
    /// <summary>
    /// Creates a default framework construction containing all
    /// the default configuration and services
    /// </summary>
    public class DefaultFrameworkConstruction : FrameworkConstruction
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFrameworkConstruction()
        {
            // Configure...
            this.Configure()
                // And add default services
                .UseDefaultServices();
        }

        #endregion
    }
}
