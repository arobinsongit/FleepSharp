using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using FleepClient.TypeClasses;
using FleepClient.Exceptions;

namespace FleepClient.MethodClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Conversation_CreateResponse : PageAPIConversationSync
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

        #region "Outputs"
        
        #endregion

        #region "Constructors"

        // No constructors

        #endregion

        #region "JSON Methods"

        #endregion




    }
}
