using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{
    // Reference : http://json2csharp.com/#

    [JsonObject(MemberSerialization.OptOut)]
    public class PageAPIConversationSyncStream : FleepTypeBase
    {
        public int trial_end_time { get; set; }
        public string mk_rec_type { get; set; }
        public string display_name { get; set; }
        public string account_id { get; set; }
        public bool is_hidden_for_add { get; set; }
        public List<string> client_flags { get; set; }
        public string phone_nr { get; set; }
        public bool has_password { get; set; }
        public string mk_email_interval { get; set; }
        public string mk_account_status { get; set; }
        public string email { get; set; }
        public string dialog_id { get; set; }
        public bool is_dialog_listed { get; set; }
        public string store_time { get; set; }
        public List<object> tags { get; set; }
        public string profile_id { get; set; }
        public BigInteger prev_message_nr { get; set; }
        public string mk_message_type { get; set; }
        public int inbox_nr { get; set; }
        public BigInteger posted_time { get; set; }
        public string conversation_id { get; set; }
        public string message { get; set; }
        public int message_nr { get; set; }
    }
}
