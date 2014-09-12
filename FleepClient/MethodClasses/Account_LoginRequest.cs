using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using Fleep.TypeClasses;
using Fleep.Exceptions;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Account_LoginRequest : MethodRequestBase
    {
        /*
                account/login
        URL: https://fleep.io/api/account/login

        Handle user login business logic. If login is successful, it sets session token and returns account info.

        Input:

        email               text            - email
        password            text            - user password
        remember_me         boolean = False - whether user wants long-term session cookie
        Output:

        account_id          text            - internal account id
        display_name        text            - name to be displayed in conversations
        profiles            list            - stream of ContactInfo records
        ticket              text            - Must be sent as parameter to all
                                              subsequent api calls
        */

        #region "Inputs"

        [JsonProperty]
        public string email { get; set; }

        [JsonProperty]
        public string password { get; set; }

        [JsonProperty]
        public bool remember_me { get; set; }

        #endregion

        #region "Utility Properties"
        /// <summary>
        /// Path to method on API
        /// </summary>
        public string MethodPath
        {
            get
            {
                return "account/login";
            }
        }
        #endregion


        #region "JSON Methods"

        #endregion




    }
}
