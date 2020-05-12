/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Web
* FileName      :  HttpWebResponseExtensions
* CreateTime    :  2020/05/12 22:03:10
************************************************************************************/

using System.IO;
using System.Net;

namespace Quan
{
    /// <summary>
    /// Extension methods for <see cref="HttpWebResponse"/>
    /// </summary>
    public static class HttpWebResponseExtensions
    {
        /// <summary>
        /// Returns a <see cref="WebRequestResult{T}"/> pre-populated with <see cref="HttpWebResponse"/> information
        /// </summary>
        /// <typeparam name="TResponse">The type of response to creates</typeparam>
        /// <param name="serverResponses">The server response</param>
        /// <returns></returns>
        public static WebRequestResult<TResponse> CreateWebRequestResult<TResponse>(this HttpWebResponse serverResponses)
        {
            // Return a new web request result
            var result = new WebRequestResult<TResponse>()
            {
                // Content type
                ContentType = serverResponses.ContentType,

                // Headers
                Headers = serverResponses.Headers,

                // Cookies
                Cookies = serverResponses.Cookies,

                // Status code
                StatusCode = serverResponses.StatusCode,

                // Status description
                StatusDescription = serverResponses.StatusDescription,

            };

            // If we got a successful response...
            if (result.StatusCode == HttpStatusCode.OK)
            {
                // Open the response stream...
                using (var responseStream = serverResponses.GetResponseStream())
                    // Get a stream reader...
                    if (responseStream != null)
                        using (var streamReader = new StreamReader(responseStream))
                            // Read in the response body
                            result.RawServerResponse = streamReader.ReadToEnd();
            }

            return result;
        }
    }
}
