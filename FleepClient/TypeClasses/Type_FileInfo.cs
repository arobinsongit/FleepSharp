using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace FleepClient.TypeClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FileInfo : FleepTypeBase
    {
        /*
        file_id             uuid            - file id received from upload
        width               bigint = None   - picure width in pixels
        height              bigint = None   - pictre width in pixels
        */

        public string file_id { get; set; }
        public BigInteger width { get; set; }
        public BigInteger height { get; set; }
    }
}
