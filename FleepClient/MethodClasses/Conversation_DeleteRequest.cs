using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using Fleep.TypeClasses;
using Fleep.Exceptions;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Conversation_DeleteRequest : MethodRequestBase
    {
        /*
       conversation/delete
       URL: https://fleep.io/api/conversation/delete/CONV_ID

       Delete conversation. Makes all content before given from_message_nr inaccessible. If you don’t leave conversation before deleting it will still reappear when someone writes here. Contents of pinboard and file drawer are not affected by this operation.

       Output:

       page_api_conversation_sync          - if from_message_nr is provided then normal
                                             forward sync otherwise only conversation
                                             state and added message
       Events:

       To all connections of all the members of this conversation.
       */

        #region Inputs

        public string ConversationID { get; set; }

        #endregion

        #region Utility Properties
        /// <summary>
        /// Path to method on API
        /// </summary>
        public string MethodPath
        {
            get
            {
                return "conversation/delete/" + this.ConversationID;
            }
        }
        #endregion

        #region JSON Methods

        #endregion




    }
}
