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
    public class Conversation_SyncRequest : MethodRequestBase
    {
        /*
       conversation/sync
        URL: https://fleep.io/api/conversation/sync/CONV_ID

        Sync state for single conversation. If used with default values 5 messages before and after last reported read_message_nr are returned. Also all conversation state are returned: PinInfo, memberInfo. All optional fields (o) are returned for first sync. After that these are included only when there have been changes. Changes are detected from system messages in message flow.

        Input:

        from_message_nr     bigint = None   - last message nr client has received.
                                              Ten next messages are read and returned
                                              If not given then 5 messages before
                                              and 5 message after read_message_nr are returned
                                              so that client can start showing relevant
                                              information to user at once.
        mk_direction        text = null     - ic_tiny - do minimal init conversation
                                                returns only inbox message and header
                                              ic_full - do full initi conversation
                                                returns inbox message and several messages
                                                before and after current read horizon
                                                and several pins and several files
                                              ic_flow - get flow fragment
                                                returns several messages before and
                                                after given from_message_nr or current
                                                read horizon if from message nr not given
                                              ic_end - get flow fragment from the end
                                                of all available content
                                              ic_backward - get flow fragment before given
                                                message
                                              ic_forward - get flow fragment after given message
                                                only visible messages are returned so not suitable
                                                for syncing cached conversation (edits will be lost)
                                              <null> - default behaviour get sequential
                                                messages forward. Returns all flow messages even
                                                non visible ones like edits of older messages
        Output:

        header              dict            - HeaderInfo record
        stream              list            - see account/poll for stream record definitions
        result_message_nr   bigint          - optional points to message that was the reason
                                              for this sync call
        hook_info           dict            - hook created by this api call
        */

        #region "Inputs"

        [JsonProperty]
        public BigInteger from_message_nr { get; set; }

        [JsonProperty]
        public string mk_direction { get; set; }

        public string ConversationID { get; set; }

        #endregion

        #region "Utility Properties"
        /// <summary>
        /// Path to method on API
        /// </summary>
        public string MethodPath
        {
            get
            {
                return "conversation/sync/" + this.ConversationID;
            }
        }
        #endregion

        #region "JSON Methods"

        #endregion




    }
}
