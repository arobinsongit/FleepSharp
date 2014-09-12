using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.Net;
using System.Collections;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

using Fleep.Exceptions;
using Fleep.TypeClasses;
using Fleep.MethodClasses;
using Fleep.UtilityMethods;

namespace Fleep
{
    public class FleepClient
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Account_LoginResponse accountLoginResponse;
        private WebHeaderCollection accountLoginWebHeaderCollection;

        private string apiURL = "https://fleep.io/api/";
        private const int validHTTPStatus = 200;

        private int lastReturnCode;

        public FleepClient()
        {
            // Do Nothing;
        }

        public FleepClient(string email, string password)
        {
            this.AccountLogin(email, password);
        }

        #region Properties

        public string TokenID
        {
            get
            {
                string resultString = "";
                string cookieString = "";

                // If header collection is null, bail with a blank
                if(accountLoginWebHeaderCollection == null)
                {
                    return "";
                }

                // Get the cookies from the header.  Return blank if it fails
                try
                {
                    cookieString = accountLoginWebHeaderCollection.Get("Set-Cookie");
                }
                catch
                {
                    return "";
                }

                try
                {
                    // Pick out token_id and trim the trailing ; b/c I'm not smart enough to figure out regex to exclude :-)
                    resultString = Regex.Match(cookieString, "token_id=(?<token_id>.*?);").Groups["token_id"].Value.TrimEnd(';');
                }
                catch
                {
                    return "";
                }

                return resultString;
            }
        }

        public string Ticket
        {
            get
            {
                if (accountLoginResponse == null)
                {
                    return "";
                }

                try{
                    return accountLoginResponse.ticket;
                }
                catch
                {
                    return "";
                }
            }
        }

        public string ApiURL
        {
            get
            {
                return apiURL;
            }
            
            set
            {
                if(value != "")
                { 
                    this.apiURL = value;
                }
            }
        }

        public bool Connected
        {
            get
            {
                return ((this.TokenID != "") && (this.Ticket != ""));
            }
        }
        
        #endregion

        /// <summary>
        /// Login to the Service
        /// </summary>
        /// <param name="Email">Login Email</param>
        /// <param name="Password">Login Password</param>
        /// <returns></returns>
        public int AccountLogin(string Email, string Password)
        {
            int returnValue;
            Account_LoginRequest accountLoginRequest;
            
            try
            {             
                using (WebClient client = new WebClient())
                {
                    
                    accountLoginRequest = new Account_LoginRequest();

                    // Set Email and Password for credentials
                    accountLoginRequest.email = Email;
                    accountLoginRequest.password = Password;

                    // Add the required default headers
                    UtilityMethods.UtilityMethods.AddDefaultHeaders(client, this.TokenID);

                    // Make the call via POST
                    String response = client.UploadString(apiURL + accountLoginRequest.MethodPath, "POST", accountLoginRequest.ToJSON());

                    // Save the Login Web Header Response for evaluation in the object
                    accountLoginWebHeaderCollection = client.ResponseHeaders;
                    
                    // Deserialize the Reponse into a LoginResponse object
                    accountLoginResponse = JsonConvert.DeserializeObject<Account_LoginResponse>(response.ToString());

                }

                returnValue = 0;
            }
            catch(Exception ex)
            {
                // Explicity clear the TokenID and Ticket
                this.accountLoginResponse = null;
                this.accountLoginWebHeaderCollection = null;
                
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

    }

}
