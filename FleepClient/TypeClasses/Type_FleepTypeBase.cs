using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Text;

namespace Fleep.TypeClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FleepTypeBase
    {
        #region "JSON Methods"
        
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this,Formatting.None);
        }

        public byte[] GetJSONBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToJSON());
        }

        public string ToJSONPrint()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion

    }
}
