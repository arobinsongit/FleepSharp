using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;

namespace Fleep.TypeClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FileInfoList : FleepTypeBase
    {
        public List<FileInfo> files { get; set; }
    }
}
