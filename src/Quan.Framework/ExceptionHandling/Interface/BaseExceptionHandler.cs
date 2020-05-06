/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.ExceptionHandling.Interface
* FileName      :  BaseExceptionHandler
* CreateTime    :  2020/05/06 21:30:19
************************************************************************************/

using System;

namespace Quan
{
    /// <summary>
    /// Handles all exceptions, simply logging them to the logger
    /// </summary>
    public class BaseExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// Logs the given exception
        /// </summary>
        /// <param name="exception">The exception to handle</param>
        public void HandleError(Exception exception)
        {
            // Log it
            // TODO: Localization of strings
            Framework.Logger.LogCriticalSource("Unhandled exception occurred", exception: exception);
        }
    }
}
