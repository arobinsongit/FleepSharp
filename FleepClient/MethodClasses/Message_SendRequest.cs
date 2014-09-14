using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using Fleep.TypeClasses;
using Fleep.Exceptions;
using System.Numerics;
using System.Collections.Generic;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Message_SendRequest : MethodRequestBase
    {
        /*
        message/send
        URL: https://fleep.io/api/message/send/CONV_ID

        Send message to flow.

        Input:

        message             text = None     - message content
        file_ids            list = None     - list of File IDs that are uploaded to Fleep
        files               list = None     - list in FileInfo objects that are uploaded
        from_message_nr     bigint = None   - used to return next batch of changes
        
        FileInfo:

        file_id             uuid            - file id received from upload
        width               bigint = None   - picure width in pixels
        height              bigint = None   - pictre width in pixels
        Output:

        page_api_conversation_sync          - if from_message_nr is provided then normal
                                              forward sync otherwise only conversation
                                              state and added message
        Errors:

        page_api_conversation_check_permissions         - common check for valid id's and membership
        Events:

        To all connections of all the members of this conversation.
        */

        #region Inputs

        [JsonProperty]
        public string message { get; set; }

        [JsonProperty]
        public BigInteger from_message_nr { get; set; }

        [JsonProperty]
        public List<string> file_ids { get; set; }

        [JsonProperty]
        public FileInfoList files { get; set; }

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
                return "message/send/" + this.ConversationID;
            }
        }
        #endregion

        #region JSON Methods

        #endregion




    }
}
