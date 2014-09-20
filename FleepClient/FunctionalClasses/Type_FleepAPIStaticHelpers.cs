using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections.Specialized;


using Fleep.MethodClasses;

namespace Fleep.TypeClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FleepAPI
    {
        public static T CallAPI<T>(Account account,string callpath, string inputJSON)
        {
            // Special call to just eat the response if the caller does not want it.
            // This is required b/c you can not have optional OUT parms on a function
            WebHeaderCollection responseEater;

            return CallAPI<T>(account, callpath, inputJSON, out responseEater);
        }

        public static T CallAPI<T>(Account account, string callpath, string inputJSON, out WebHeaderCollection responseHeaderCollection)
        {
            T returnValue;
            string response;
            bool isAccountLogin;
            
            try
            {
                using (WebClient wc = new WebClient())
                {

                    //Figure out if this is an account login
                    isAccountLogin = (typeof(Account_LoginResponse).IsAssignableFrom(typeof(T)));

                    // If this is not a login attempt then check the connection and add the ticket
                    if (!isAccountLogin)               
                    {
                        if (!account.Connected)
                        {
                            throw new Exceptions.NotLoggedInException();
                        }

                        // Add the Ticket
                        inputJSON = UpdateTicketinJSON(account, inputJSON);
                    }

                    // Add the required default headers
                    AddDefaultHeaders(wc, account);

                    // Make the call via POST
                    response = wc.UploadString(account.ApiURL + callpath, "POST", inputJSON);

                    // Save the Login Web Header Response for evaluation in the object
                    responseHeaderCollection = wc.ResponseHeaders;
                }

                returnValue = JsonConvert.DeserializeObject<T>(response);

                // Return the result value, should only be 0
                return returnValue;
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        public static File_UploadResponse UploadFile(Account account, File_UploadRequest fileUploadRequest)
        {                      
            try
            {                
                File_UploadResponse fileUploadResponse;
                File_RenameResponse fileRenameResponse;

                using (WebClient wc = new WebClient())
                {
                    if (!account.Connected)
                    {
                        throw new Exceptions.NotLoggedInException();
                    }

                    // Add the Ticket
                    NameValueCollection parameters = new NameValueCollection();
                    parameters.Add("ticket",account.Ticket);
                    wc.QueryString = parameters;
            
                    // Add the required default headers
                    AddDefaultHeaders(wc, account);

                    // Make the call via PUT
                    byte[] responseBytes = wc.UploadFile(account.ApiURL + fileUploadRequest.MethodPath, "PUT", fileUploadRequest.filepath);

                    // Deserialize the Results
                    fileUploadResponse = JsonConvert.DeserializeObject<File_UploadResponse>(Encoding.ASCII.GetString(responseBytes));              

                    //Force a rename TODO - When they finish updating the file interface
                    //File_RenameRequest fileRenameRequest = new File_RenameRequest();
                    //fileRenameRequest.ConversationID = fileUploadRequest.ConversationID;

                    //fileRenameRequest.file_id = fileUploadResponse.files[0].file_id;
                    //fileRenameRequest.file_name = fileUploadRequest.filename;
                    
                    //fileRenameResponse = CallAPI<File_RenameResponse>(account, fileRenameRequest.MethodPath, fileRenameRequest.ToJSON());

                    // Return the Results 
                    return fileUploadResponse;
                    
                }
            }
            catch
            {
                // Throw the exception
                throw;
            }

        }

        #region Utility Methods

        /// <summary>
        /// Inject the ticket into an existing JSON string
        /// </summary>
        /// <param name="client"></param>
        /// <param name="workingJSON"></param>
        private static string UpdateTicketinJSON(Account account, string inputJSON)
        {
            try
            {
                var results = JsonConvert.DeserializeObject<dynamic>(inputJSON);
                results.ticket = account.Ticket;
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
        private static WebClient AddDefaultHeaders(WebClient wc, Account account)
        {
            try
            {
                // Set the header to JSON
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                // Add the token
                wc.Headers.Add(HttpRequestHeader.Cookie, "token_id=" + account.TokenID);

                return wc;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region JSON Methods

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
