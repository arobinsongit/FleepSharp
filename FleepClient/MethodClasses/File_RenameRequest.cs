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
    public class File_RenameRequest : MethodRequestBase
    {
        /*
        file/rename
        URL: https://fleep.io/api/file/rename/CONV_ID

        Rename file in this conversation

        Input:

        file_id             uuid            - File ID to be renamed
        file_name           text            - new name for the file
        from_message_nr     bigint = None   - earliest message nr client has received.
                                              next messages are read and returned
        Output:

        header              dict            - HeaderInfo record
        stream              list            - see account/poll for stream record definitions
        */

        #region Inputs

        [JsonProperty]
        public string file_id { get; set; }

        [JsonProperty]
        public string file_name { get; set; }

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
                return "file/rename/" + this.ConversationID;
            }
        }
        #endregion

        #region JSON Methods

        #endregion




    }
}
