using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using Fleep.TypeClasses;
using System.Numerics;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class File_RenameResponse : PageAPIConversationSync
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

        #region Outputs

        public string file_id { get; set; }
        public string file_name { get; set; }
        public BigInteger from_message_nr { get; set; }

        #endregion

        #region Constructors

        // No constructors

        #endregion

        #region JSON Methods

        #endregion




    }
}
