using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using Fleep.TypeClasses;
using Fleep.Exceptions;
using System.Numerics;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Conversation_SyncBackwardRequest : MethodRequestBase
    {
        /*
        conversation/sync_backward
        URL: https://fleep.io/api/conversation/sync_backward/CONV_ID

        Sync state for single conversation. Used to fetch messages for backward scroll.

        Input:

        from_message_nr     bigint = None   - earliest message nr client has received.
                                              previous messages are read and returned
        Output:

        header              dict            - HeaderInfo record
        stream              list            - see account/poll for stream record definitions
        */

        #region Inputs

        [JsonProperty]
        public BigInteger from_message_nr { get; set; }

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
                return "conversation/sync_backward/" + this.ConversationID;
            }
        }
        #endregion

        #region JSON Methods

        #endregion




    }
}
