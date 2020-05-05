/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Dna.Logging.File
* FileName      :  FileLoggerConfiguration
* CreateTime    :  2020/05/05 12:07:07
************************************************************************************/

using Microsoft.Extensions.Logging;

namespace Quan
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerConfiguration
    {
        #region Public Properties

        /// <summary>
        /// The level of log that should be processed
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;


        /// <summary>
        /// The path to write to
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Whether to log the time as part of the message
        /// </summary>
        public bool LogTime { get; set; } = true;

        #endregion
    }
}
