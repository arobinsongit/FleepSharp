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
    public class Conversation_DeleteResponse : PageAPIConversationSync
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

        #region Outputs

        #endregion

        #region Constructors

        // No constructors

        #endregion

        #region JSON Methods

        #endregion

    }
}
