using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Text;


//"profiles":[{"mk_rec_type":"contact","display_name":"Andy Robinson (Phase2)","account_id":"6ea04d6c-0c4a-4a10-b8a1-b45a7689afc9","is_hidden_for_add":false,"client_flags":["tip-clist-large","tip-newconv"],"avatar_urls":null,"mk_email_interval":"default","mk_account_status":"active","email":"andy@phase2automation.com"}]   

namespace FleepClient.TypeClasses
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
