using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Text;
using FleepClient.TypeClasses;
using FleepClient.Exceptions;


namespace FleepClient.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MethodRequestBase
    {
        [JsonProperty]
        public string ticket { get; set; }

        #region "Methods"

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
