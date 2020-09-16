﻿/************************************************************************************
* Copyright (c) 2020 quanljh@gmail.com All Rights Reserved.
* Author        :  quanljh
* NameSpace     :  Quan.Web
* FileName      :  WebRequests
* CreateTime    :  2020/05/07 22:02:09
************************************************************************************/

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quan.Web
{
    /// <summary>
    /// Provides HTTP calls for sending and receiving information from a HTTP server
    /// </summary>
    public static class WebRequests
    {
        /// <summary>
        /// Posts a web request to an URL and returns the raw http web response
        /// </summary>
        /// <param name="url">The URL to post to</param>
        /// <param name="content">The content to post</param>
        /// <param name="sendType">The format to serialize the content into</param>
        /// <param name="returnType">The expected type of content to be returned from the server</param>
        /// <param name="configureRequest">Allows caller to customize and configure the request prior to the content being writter and sent</param>
        /// <param name="bearerToken">If specified, provides the Authorization header with 'bearer token-here' for things like JWT bearer tokens</param>
        /// <returns></returns>
        public static async Task<HttpWebResponse> PostAsync(string url, object content = null,
            KnownContentSerializers sendType = KnownContentSerializers.Json,
            KnownContentSerializers returnType = KnownContentSerializers.Json,
            Action<HttpWebRequest> configureRequest = null,
            string bearerToken = null)
        {
            #region Setup

            // Create the web request
            var request = WebRequest.CreateHttp(url);

            // Make it a POST request method
            request.Method = HttpMethod.Post.ToString();

            // Set the appropriate return type
            request.Accept = returnType.ToMimeString();

            // Set the content type 
            request.ContentType = sendType.ToMimeString();

            // If we have a bearer token...
            if (bearerToken != null)
                // Add bearer token to header
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {bearerToken}");

            // Any custom work
            configureRequest?.Invoke(request);

            #endregion

            #region Writer Content

            // Set the content length
            if (content == null)
            {
                // Set content length to 0
                request.ContentLength = 0;
            }
            // Otherwise...
            else
            {
                // Create content to write
                var contentString = string.Empty;

                // Serialize to Json ?
                if (sendType == KnownContentSerializers.Json)
                    // Serialize content to Json string
                    contentString = JsonConvert.SerializeObject(content);
                // Serialize to Xml ?
                else if (sendType == KnownContentSerializers.Xml)
                {
                    // Create Xml serializer
                    var xmlSerializer = new XmlSerializer(content.GetType());

                    // Create a string writer to receive the serialized string
                    using (var stringWriter = new StringWriter())
                    {
                        // Serialize the object to a string
                        xmlSerializer.Serialize(stringWriter, content);

                        // Extract the string from the writer
                        contentString = stringWriter.ToString();
                    }
                }
                // Currently unknown
                else
                {
                    // TODO: Throw error once we have Dna Framework exception types
                }

                // Write content to HTTP body stream
                using (var requestString = await request.GetRequestStreamAsync())
                // Create a string writer from the body stream...
                using (var streamWriter = new StreamWriter(requestString))
                    // Write content to HTTP body stream
                    await streamWriter.WriteAsync(contentString);
            }

            #endregion

            // Wrap call...
            try
            {
                // Return the raw server response
                return await request.GetResponseAsync() as HttpWebResponse;
            }
            // Catch Web Exceptions (which throw for things like 401)
            catch (WebException ex)
            {
                // And instead, return the response and let the caller decide what to do with the StatusCode
                return ex.Response as HttpWebResponse;
            }
        }


        /// <summary>
        /// Posts a web request to an URL and returns the a response of the expected data type
        /// </summary>
        /// <param name="url">The URL to post to</param>
        /// <param name="content">The content to post</param>
        /// <param name="sendType">The format to serialize the content into</param>
        /// <param name="returnType">The expected type of content to be returned from the server</param>
        /// <param name="configureRequest">Allows caller to customize and configure the request prior to the content being writter and sent</param>
        /// <param name="bearerToken">If specified, provides the Authorization header with 'bearer token-here' for things like JWT bearer tokens</param>
        /// <returns></returns>
        public static async Task<WebRequestResult<TResponse>> PostAsync<TResponse>(string url, object content = null,
            KnownContentSerializers sendType = KnownContentSerializers.Json,
            KnownContentSerializers returnType = KnownContentSerializers.Json,
            Action<HttpWebRequest> configureRequest = null,
        string bearerToken = null)
        {
            // Make the standard Post call first
            var serverResponse = await PostAsync(url, content, sendType, returnType, configureRequest, bearerToken);

            // Create a result
            var result = serverResponse.CreateWebRequestResult<TResponse>();

            // If the response status code is not 200...
            if (result.StatusCode != HttpStatusCode.OK)
            {
                // Call failed
                // Return no error message so the client can display its own based on the status code

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // If the server response type was not the expected type...
                if (!serverResponse.ContentType.ToLower().Contains(returnType.ToMimeString().ToLower()))
                {
                    // Fail due to unexpected return type
                    result.ErrorMessage =
                        $"Server did not return data in expected type. Expected {returnType.ToMimeString()}, received {serverResponse.ContentType}";

                    // Done
                    return result;
                }

                // Json?
                if (returnType == KnownContentSerializers.Json)
                {
                    // Deserialize json String
                    result.ServerResponse = JsonConvert.DeserializeObject<TResponse>(result.RawServerResponse);
                }
                // Xml ?
                else if (returnType == KnownContentSerializers.Xml)
                {
                    // Create Xml serializer
                    var xmlSerializer = new XmlSerializer(typeof(TResponse));

                    // Create a memory stream for the raw string data
                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.RawServerResponse)))
                    {
                        // Deserialize XML string
                        result.ServerResponse = (TResponse)xmlSerializer.Deserialize(memoryStream);
                    }

                }
                else
                {
                    // If deserialize failed then set error message
                    result.ErrorMessage =
                        "Unknown return type, cannot deserialize server response to the expected type";

                    // Done
                    return result;
                }
            }
            catch (Exception e)
            {
                // If deserialize failed then set error message
                result.ErrorMessage = "Failed to deserialize server response to the expected type";
            }

            return result;
        }
    }
}
