using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Net;

namespace FleepClient.UtilityMethods
{
    public class UtilityMethods
    {
        public static WebClient AddDefaultHeaders(WebClient client, string tokenID)
        {
            // Set the header to JSON
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            // Add the token
            client.Headers.Add(HttpRequestHeader.Cookie, "token_id=" + tokenID);

            return client;
        }
    }

}
