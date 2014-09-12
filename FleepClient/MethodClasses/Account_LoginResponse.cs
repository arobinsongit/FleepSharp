using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using FleepClient.TypeClasses;
using FleepClient.Exceptions;
using System.Text.RegularExpressions;

namespace FleepClient.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Account_LoginResponse : MethodResponseBase
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

        #region "Outputs"

        [JsonProperty]
        public string account_id { get; set; }

        [JsonProperty]
        public string display_name { get; set; }

        [JsonProperty]
        public List<AccountProfile> profiles { get; set; }

        [JsonProperty]
        public string ticket { get; set; }
        
        #endregion

        
        #region "Constructors"

        // No constructors

        #endregion

        #region "JSON Methods"

        #endregion


    }
}
