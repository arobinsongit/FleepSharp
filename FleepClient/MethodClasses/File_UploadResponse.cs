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
    public class File_UploadResponse : PageAPIConversationSync
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

        #region Outputs

        public List<FileInfo> files { get; set; }

        #endregion

        #region Constructors

        // No constructors

        #endregion

        #region JSON Methods

        #endregion




    }
}
