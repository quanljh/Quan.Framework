/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.ExceptionHandling.Interface
* FileName      :  IExceptionHandler
* CreateTime    :  2020/05/06 21:28:14
************************************************************************************/

using System;

namespace Quan
{
    /// <summary>
    /// Handles exceptions when they are caught and passed to the exception handler
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Handles the given exception
        /// </summary>
        /// <param name="exception">The exception to handle</param>
        void HandleError(Exception exception);
    }
}
