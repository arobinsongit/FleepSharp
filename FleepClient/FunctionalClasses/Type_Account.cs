using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;
using Fleep.MethodClasses;
using Fleep.TypeClasses;
using Fleep.Exceptions;
using Fleep.UtilityMethods;
using System.Net;
using System.Text.RegularExpressions;


namespace Fleep.TypeClasses
{
    // Reference : http://json2csharp.com/#

    [JsonObject(MemberSerialization.OptOut)]
    public class Account : FleepFunctionalTypeBase
    {

        #region Declarations

        private Account_LoginResponse lastAccountLoginResponse;
        private Account_PollResponse lastAccountPollResponse;
        private WebHeaderCollection accountLoginWebHeaderCollection;
        
        // Allow for setting Token and Ticket externally without forcing login
        private string tokenIDExternalSet = "";
        private string ticketExternalSet = "";

        #endregion

        #region Constructors

        public Account()
        {
            this.Initialize();
        }

        public Account(string email, string password)
        {
            this.Initialize(email,password);
        }

        #endregion

        #region Initializtion Functions

        private void Initialize()
        {
            //Empty initializer
        }

        private void Initialize(string email, string password)
        {
            this.Login(email, password);        
        }

        #endregion

        #region Properties

        public string TokenID
        {
            get
            {
                string resultString = "";
                string cookieString = "";

                // If the external TokenID has been explicitly set then return this
                if (this.tokenIDExternalSet != "")
                {
                    return this.tokenIDExternalSet;
                }

                // If header collection is null, bail with a blank
                if (this.accountLoginWebHeaderCollection == null)
                {
                    return "";
                }

                // Get the cookies from the header.  Return blank if it fails
                try
                {
                    cookieString = this.accountLoginWebHeaderCollection.Get("Set-Cookie");
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

            set
            {
                if (value != "")
                {
                    tokenIDExternalSet = value;
                }
            }
        }

        public string Ticket
        {
            get
            {
                //If the Ticket has been set externally then return that
                if (ticketExternalSet != "")
                {
                    return ticketExternalSet;
                }

                if (this.lastAccountLoginResponse == null)
                {
                    return "";
                }

                try
                {
                    return this.lastAccountLoginResponse.ticket;
                }
                catch
                {
                    return "";
                }
            }

            set
            {
                if (value != "")
                {
                    ticketExternalSet = value;
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

        public Account_LoginResponse LastAccountLoginResponse
        {
            get { return this.lastAccountLoginResponse; }
        }

        public Account_PollResponse LastAccountPollResponse
        {
            get { return this.lastAccountPollResponse ; }
        }

        public WebHeaderCollection AccountLoginWebHeaderCollection
        {
            get { return this.accountLoginWebHeaderCollection; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Login to the Service
        /// </summary>
        /// <param name="email">Login email</param>
        /// <param name="Password">Login Password</param>
        /// <returns></returns>
        public Account_LoginResponse Login(string email, string password)
        {
            try
            {
                return Login(email,password, out accountLoginWebHeaderCollection);
            }
            catch
            {
                throw;
            }
        }

          /// <summary>
        /// Login to the Service
        /// </summary>
        /// <param name="email">Login email</param>
        /// <param name="Password">Login Password</param>
        /// <returns></returns>
        public Account_LoginResponse Login(string email, string password, out WebHeaderCollection accountLoginWebHeaderCollection)
        {
            try
            {
                Account_LoginRequest accountLoginRequest = new Account_LoginRequest();

                // Set email and Password for credentials
                accountLoginRequest.email = email;
                accountLoginRequest.password = password;

                // Call the API
                this.lastAccountLoginResponse  = FleepAPI.CallAPI<Account_LoginResponse>(this, accountLoginRequest.MethodPath, accountLoginRequest.ToJSON(), out accountLoginWebHeaderCollection);
                this.accountLoginWebHeaderCollection = accountLoginWebHeaderCollection;

                return this.lastAccountLoginResponse;

            }
            catch
            {
                // Explicity clear the TokenID and Ticket
                this.lastAccountLoginResponse = null;
                this.accountLoginWebHeaderCollection = null;

                // Throw the exception
                throw;
            }
        }

        #endregion

        #region Utility Functions

        #endregion

    }
}
