/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Dna.Logging.File
* FileName      :  FileLoggerProvider
* CreateTime    :  2020/05/05 12:04:28
************************************************************************************/

using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Quan
{
    /// <summary>
    /// Provides the ability to log to file
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        #region Protected Members

        /// <summary>
        /// The path to log to
        /// </summary>
        protected string mFilePath;

        /// <summary>
        /// The configuration to use when creating loggers
        /// </summary>
        protected readonly FileLoggerConfiguration mConfiguration;

        /// <summary>
        /// Keeps track of the logger already created
        /// </summary>
        protected readonly ConcurrentDictionary<string, FileLogger> mLoggers = new ConcurrentDictionary<string, FileLogger>();
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="path">The path to log to</param>
        /// <param name="configuration">The configuration to use</param>
        public FileLoggerProvider(string path, FileLoggerConfiguration configuration)
        {
            // Set the path
            mFilePath = path;

            // Set the configuration
            mConfiguration = configuration;
        }

        #endregion

        #region ILoggerProvider Implementation

        /// <summary>
        /// Creates a file logger based on the category name
        /// </summary>
        /// <param name="categoryName">The category name of this logger</param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return mLoggers.GetOrAdd(categoryName, name => new FileLogger(name, mFilePath, mConfiguration));
        }

        #endregion

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Clear the list of loggers
            mLoggers.Clear();
        }

    }
}
