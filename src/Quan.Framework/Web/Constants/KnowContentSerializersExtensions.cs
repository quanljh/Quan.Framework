/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Web.Constants
* FileName      :  KnowContentSerializersExtensions
* CreateTime    :  2020/05/11 21:48:08
************************************************************************************/

namespace Quan
{
    /// <summary>
    /// Extension methods for <see cref="KnownContentSerializers"/>
    /// </summary>
    public static class KnowContentSerializersExtensions
    {
        /// <summary>
        /// Takes a known serializer type and returns the Mime type associated with it
        /// </summary>
        /// <param name="serializers"></param>
        /// <returns></returns>
        public static string ToMimeString(this KnownContentSerializers serializers)
        {
            // Switch on the serializer
            switch (serializers)
            {
                case KnownContentSerializers.Json:
                    return "application/json";

                case KnownContentSerializers.Xml:
                    return "application/xml";

                default:
                    return "application/octet-stream";
            }
        }
    }
}
