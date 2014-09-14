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
    public class File_UploadRequest : MethodRequestBase
    {
        /*
        file/upload
        URL: https://fleep.io/api/file/upload/CONV_ID

        Upload one or more files. Returns file_id which needs later given to /api/file/send.

        Optional URL arguments:

        ticket=XXX Access ticket _method=PUT This POST is actually PUT
        POST + multipart/form-data:

        files - input field for files Content-Type, Content-Disposition are taken from subsection header.
        PUT:
        Content-Type, Content-Disposition are taken from main header
        Output:

        files - array of:
            file_id
            name
            size
        */

        #region Inputs

        //[JsonProperty]
        //public string message { get; set; }

        //[JsonProperty]
        //public BigInteger from_message_nr { get; set; }

        //[JsonProperty]
        //public List<string> file_ids { get; set; }
        
        //public FileInfoList files { get; set; }

        public string filepath { get; set; }

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
                return "file/upload/" + this.ConversationID;
            }
        }
        #endregion

        #region JSON Methods

        #endregion




    }
}
