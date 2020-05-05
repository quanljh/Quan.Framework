/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Dna.Logging
* FileName      :  LoggerSourceFormatter
* CreateTime    :  2020/05/05 14:41:50
************************************************************************************/

using System;
using System.IO;

namespace Quan
{
    /// <summary>
    /// Formats a message when the callers source information is provider first in the arguments
    /// </summary>
    public static class LoggerSourceFormatter
    {
        /// <summary>
        /// Formats the message including the source information pulled out of the state
        /// </summary>
        /// <param name="state">The state information about the log</param>
        /// <param name="exception">The exception</param>
        /// <returns></returns>
        public static string Format(object[] state, Exception exception)
        {
            // Get the values from the state
            var origin = (string)state[0];
            var filePath = (string)state[1];
            var lineNumber = (int)state[2];
            var message = (string)state[3];

            // Get any exception message
            var exceptionMessage = exception?.ToString();

            // New line if we have an exception ...
            if (exception != null)
                // New line between message and exception
                message += Environment.NewLine + exceptionMessage;

            // Format the message string
            return $"{message}  [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]{Environment.NewLine}";

        }
    }
}
