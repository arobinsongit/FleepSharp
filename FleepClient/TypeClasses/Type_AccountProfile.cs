using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;

namespace FleepClient.TypeClasses
{
    // Reference : http://json2csharp.com/#

    [JsonObject(MemberSerialization.OptOut)]
    public class AccountProfile : FleepTypeBase
    {
        public string mk_rec_type { get; set; }
        public string display_name { get; set; }
        public string account_id { get; set; }
        public bool is_hidden_for_add { get; set; }
        public List<string> client_flags { get; set; }
        public object avatar_urls { get; set; }
        public string mk_email_interval { get; set; }
        public string mk_account_status { get; set; }
        public string email { get; set; }
    }
}
