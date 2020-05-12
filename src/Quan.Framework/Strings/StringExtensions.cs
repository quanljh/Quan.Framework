/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Strings
* FileName      :  StringExtensions
* CreateTime    :  2020/05/12 22:21:20
************************************************************************************/

namespace Quan
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string is null or an empty string
        /// </summary>
        /// <param name="content">The string</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string content)
        {
            return string.IsNullOrEmpty(content);
        }

        /// <summary>
        /// Returns true if the string is null or an empty string or just whitespace
        /// </summary>
        /// <param name="content">The string</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string content)
        {
            return string.IsNullOrWhiteSpace(content);
        }

    }
}
