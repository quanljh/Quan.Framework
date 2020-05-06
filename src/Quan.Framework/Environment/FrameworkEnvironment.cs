/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Dna
* FileName      :  FrameworkEnvironment
* CreateTime    :  2020/05/05 10:58:59
************************************************************************************/

namespace Quan
{
    /// <summary>
    /// Details about the current system environment
    /// </summary>
    public class FrameworkEnvironment
    {
        #region Public Properties

        /// <summary>
        /// True if we are in a development environment
        /// </summary>
        public bool IsDevelopment { get; set; } = true;

        /// <summary>
        /// The configuration of the environment, either Development or Production
        /// </summary>
        public string Configuration => IsDevelopment ? "Development" : "Production";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FrameworkEnvironment()
        {
#if RELEASE
            IsDevelopment = false;
#endif
        }

        #endregion
    }
}
