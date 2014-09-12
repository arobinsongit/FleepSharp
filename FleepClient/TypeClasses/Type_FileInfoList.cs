using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;

namespace FleepClient.TypeClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FileInfoList : FleepTypeBase
    {
        public List<FileInfo> list { get; set; }
    }
}
