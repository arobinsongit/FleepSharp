using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using Fleep.TypeClasses;
using Fleep.Exceptions;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Conversation_CreateRequest : MethodRequestBase
    {
        /*
        conversation/create
        URL: https://fleep.io/api/conversation/create

        Create new conversation

        Input:

        topic               text = None         - conversation topic
        emails              text = None         - list of email addresses like on email To: line
        message             text = None         - Initial message into the conversation
        files               list = None         - list of files to be added to conversation
        is_invite           boolean = None      - Send out invite emails to fresh fleepers
        Output:

        page_api_conversation_sync          - if from_message_nr is provided then normal
                                              forward sync otherwise only conversation
                                              state and added message
        Events:

        To all connections of all the members of this conversation.
        */

        #region "Inputs"

        [JsonProperty]
        public string topic { get; set; }

        [JsonProperty]
        public string emails { get; set; }

        [JsonProperty]
        public string message { get; set; }

        //TODO
        //[JsonProperty]
        //public string files { get; set; }


        [JsonProperty]
        public bool is_invite { get; set; }

        #endregion

        #region "Utility Properties"
        /// <summary>
        /// Path to method on API
        /// </summary>
        public string MethodPath
        {
            get
            {
                return "conversation/create";
            }
        }
        #endregion

        #region "JSON Methods"

        #endregion




    }
}
