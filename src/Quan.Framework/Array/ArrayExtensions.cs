/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Dna.Array
* FileName      :  ArrayExtensions
* CreateTime    :  2020/05/05 14:32:48
************************************************************************************/

using System.Linq;

namespace Quan
{
    /// <summary>
    /// Extensions methods for arrays
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Append the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="source">The original array of values</param>
        /// <param name="toAdd">The values to append to the source</param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] source, params T[] toAdd)
        {
            // Append and return the new items
            return source.Concat(toAdd).ToArray();
        }


        /// <summary>
        /// Prepend the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="source">The original array of values</param>
        /// <param name="toAdd">The values to prepend to the source</param>
        /// <returns></returns>
        public static T[] Prepend<T>(this T[] source, params T[] toAdd)
        {
            // Prepend and return the new items
            return toAdd.Append(source);
        }
    }
}
