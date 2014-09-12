using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Fleep.TypeClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FleepAPI
    {
        public static T CallAPI<T>(FleepClient client,string callpath, string inputJSON)
        {
            // Special call to just eat the response if the caller does not want it.
            // This is required b/c you can not have optional OUT parms on a function
            WebHeaderCollection responseEater;

            return CallAPI<T>(client, callpath, inputJSON, out responseEater);
        }

        public static T CallAPI<T>(FleepClient client, string callpath, string inputJSON, out WebHeaderCollection responseHeaderCollection)
        {
            T returnValue;
            string response;
            
            if (!client.Connected)
            {
                throw new Exceptions.NotLoggedInException();
            }

            try
            {
                using (WebClient wc = new WebClient())
                {
                    // Add the required default headers
                    AddDefaultHeaders(wc, client);

                    // Add the Ticket
                    inputJSON = UpdateTicketinJSON(client, inputJSON);

                    // Make the call via POST
                    response = wc.UploadString(client.ApiURL + callpath, "POST", inputJSON);

                    // Save the Login Web Header Response for evaluation in the object
                    responseHeaderCollection = wc.ResponseHeaders;
                }

                returnValue = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception ex)
            {
                // Throw the exception
                throw ex;
            }

            finally
            {
                // Do Nothing   
            }

            // Return the result value, should only be 0
            return returnValue;
        }

        #region Utility Methods

        /// <summary>
        /// Inject the ticket into an existing JSON string
        /// </summary>
        /// <param name="client"></param>
        /// <param name="workingJSON"></param>
        private static string UpdateTicketinJSON(FleepClient client, string inputJSON)
        {
            try
            {
                var results = JsonConvert.DeserializeObject<dynamic>(inputJSON);
                results.ticket = client.Ticket;
                return JsonConvert.SerializeObject(results);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add default headers to web call
        /// </summary>
        /// <param name="client"></param>
        /// <param name="tokenID"></param>
        /// <returns></returns>
        private static WebClient AddDefaultHeaders(WebClient wc, FleepClient client)
        {
            try
            {
                // Set the header to JSON
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                // Add the token
                wc.Headers.Add(HttpRequestHeader.Cookie, "token_id=" + client.TokenID);

                return wc;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region "JSON Methods"

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this,Formatting.None);
        }

        public byte[] GetJSONBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToJSON());
        }

        public string ToJSONPrint()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion

    }
}
