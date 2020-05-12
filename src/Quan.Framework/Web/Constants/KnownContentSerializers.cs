/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Web.Constants
* FileName      :  KnownContentType
* CreateTime    :  2020/05/07 22:06:59
************************************************************************************/

namespace Quan
{
    /// <summary>
    /// Knows types of content that can be serialized and sent to a receiver
    /// </summary>
    public enum KnownContentSerializers
    {
        /// <summary>
        /// The data should be serialized as JSON
        /// </summary>
        Json = 1,

        /// <summary>
        /// The data should be serialized to XML
        /// </summary>
        Xml
    }
}
