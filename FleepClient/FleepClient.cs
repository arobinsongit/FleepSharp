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

        private Account account = new Account();

        // Allow for setting Token and Ticket externally without forcing login
        private string tokenIDExternalSet = "";
        private string ticketExternalSet = "";

        private string apiURL = "https://fleep.io/api/";
        private const int validHTTPStatus = 200;

        private int lastReturnCode;

        public FleepClient()
        {
            // Do Nothing;
        }

        public FleepClient(string email, string password)
        {
            this.Login(email, password);
        }

        #region Properties

        public string TokenID
        {
            get
            {
                return account.TokenID;
            }

            set
            {
                if (value != "")
                {
                    account.TokenID = value;
                }
            }
        }

        public string Ticket
        {
            get
            {
                return account.Ticket;
            }

            set
            {
                if (value != "")
                {
                    account.Ticket = value;
                }
            }
        }

        public bool Connected
        {
            get
            {
                return account.Connected;
            }
        }

        public Account Account
        {
            get
            {
               return this.account;
            }

            set
            {
                if (value != null)
                {
                    this.account = value;
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
                if (value != "")
                {
                    this.apiURL = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// Login to the Service
        /// </summary>
        /// <param name="email">Login email</param>
        /// <param name="Password">Login Password</param>
        /// <returns></returns>
        public bool Login(string email, string password)
        {
            try
            {
                account.ApiURL = this.ApiURL;
                account.Login(email, password);
                return account.Connected;
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
