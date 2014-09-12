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
    public class PageAPIConversationSyncHeader : FleepTypeBase
    {
        public string mk_alert_level { get; set; }
        public string profile_id { get; set; }
        public string topic { get; set; }
        public List<object> guests { get; set; }
        public double last_message_time { get; set; }
        public string cmail { get; set; }
        public bool can_post { get; set; }
        public int read_message_nr { get; set; }
        public int fw_message_nr { get; set; }
        public bool is_tiny { get; set; }
        public int last_message_nr { get; set; }
        public int join_message_nr { get; set; }        
        public string mk_rec_type { get; set; }
        public string account_id { get; set; }
        public bool is_init { get; set; }
        public int last_inbox_nr { get; set; }
        public List<string> members { get; set; }
        public string conversation_id { get; set; }
        public int send_message_nr { get; set; }
        public int bw_message_nr { get; set; }
        public List<object> teams { get; set; }
        public int unread_count { get; set; }
        public List<object> leavers { get; set; }
        public int inbox_message_nr { get; set; }
        public int show_message_nr { get; set; }
    }
}
