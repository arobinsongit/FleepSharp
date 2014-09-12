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
        public void AccountLogin(string Email, string Password)
        {
            try
            {
                Account_LoginRequest accountLoginRequest = new Account_LoginRequest();

                // Set Email and Password for credentials
                accountLoginRequest.email = Email;
                accountLoginRequest.password = Password;

                //Create a fake fleepclient

                // Call the API
                this.accountLoginResponse = FleepAPI.CallAPI<Account_LoginResponse>(this, accountLoginRequest.MethodPath, accountLoginRequest.ToJSON(),out accountLoginWebHeaderCollection);
            }
            catch
            {
                // Explicity clear the TokenID and Ticket
                this.accountLoginResponse = null;
                this.accountLoginWebHeaderCollection = null;

                // Throw the exception
                throw;
            }
        }

    }

}
