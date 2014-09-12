using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using Fleep.TypeClasses;
using Fleep.Exceptions;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Conversation_SyncResponse : PageAPIConversationSync
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

        #region "Outputs"

        #endregion

        #region "Constructors"

        // No constructors

        #endregion

        #region "JSON Methods"

        #endregion




    }
}
