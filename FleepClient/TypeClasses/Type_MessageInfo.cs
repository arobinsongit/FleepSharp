using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{


    /*
     
     MessageInfo:

mk_rec_type         text = 'message'
conversation_id     text            - mostly needed for conversation create case
message_nr          bigint          - message sequence number in message flow
message             text            - message body - XML.
account_id          text            - Author ID
mk_message_type     text            - see Classificators
flow_message_nr     bigint          - In case this message is edit or delete of one of the earlier messages
revision_message_nr bigint          - Latest visible revision
posted_time         bigint          - unix timestamp in seconds
pin_weight          numeric         - may be filled for pin messages
                                      apply only if filled
ref_message_nr      bigint          - used by messages that need ot reference other
                                      messages like unpin message fro example
edit_account_id     text            - ref to message revisor, mormally same as author but
                                      may be different for pinned messages and files
edited_time         bigint          - message edit time
tags                text[]          - list of message flags. Possible values:
                                        pin - message has been pinned
                                        unpin - message has been unpinned
                                        unlock - message has been unlocked
lock_account_id     text            - in case pin is locked for editing
inbox_nr            bigint          - inbox number of message. Only alerting messages are
                                      assigned inbox numbers. So last_inbox_nr - inbox_nr
                                      should give number of alerting messages.
                                      Negative values point to previous inbox number from
                                      this message.
hook_key            text            - filled if message was sent via hook
prev_message_nr     bigint          - number of previous visible message
is_new_sheet        boolean         - start new sheet in message flow
     
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class MessageInfo : FleepTypeBase
    {        
    ///<remarks>
    ///</remarks>
    public string mk_rec_type {get; set;}
    ///<remarks>
    /// mostly needed for conversation create case
    ///</remarks>
    public string conversation_id {get; set;}
    ///<remarks>
    /// message sequence number in message flow
    ///</remarks>
    public BigInteger message_nr {get; set;}
    ///<remarks>
    /// message body - XML.
    ///</remarks>
    public string message {get; set;}
    ///<remarks>
    /// Author ID
    ///</remarks>
    public string account_id {get; set;}
    ///<remarks>
    /// see Classificators
    ///</remarks>
    public string mk_message_type {get; set;}
    ///<remarks>
    /// In case this message is edit or delete of one of the earlier messages
    ///</remarks>
    public BigInteger flow_message_nr {get; set;}
    ///<remarks>
    /// Latest visible revision
    ///</remarks>
    public BigInteger revision_message_nr {get; set;}
    ///<remarks>
    /// unix timestamp in seconds
    ///</remarks>
    public BigInteger posted_time {get; set;}
    ///<remarks>
    /// may be filled for pin messages
    /// apply only if filled
    ///</remarks>
    public int pin_weight {get; set;}
    ///<remarks>
    /// used by messages that need ot reference other
    /// messages like unpin message fro example
    ///</remarks>
    public BigInteger ref_message_nr {get; set;}
    ///<remarks>
    /// ref to message revisor, mormally same as author but
    /// may be different for pinned messages and files
    ///</remarks>
    public string edit_account_id {get; set;}
    ///<remarks>
    /// message edit time
    ///</remarks>
    public BigInteger edited_time {get; set;}
    ///<remarks>
    /// list of message flags. Possible values:
    /// pin - message has been pinned
    /// unpin - message has been unpinned
    /// unlock - message has been unlocked
    ///</remarks>
    public List<String> tags {get; set;}
    ///<remarks>
    /// in case pin is locked for editing
    ///</remarks>
    public string lock_account_id {get; set;}
    ///<remarks>
    /// inbox number of message. Only alerting messages are
    /// assigned inbox numbers. So last_inbox_nr - inbox_nr
    /// should give number of alerting messages.
    /// Negative values point to previous inbox number from
    /// this message.
    ///</remarks>
    public BigInteger inbox_nr {get; set;}
    ///<remarks>
    /// filled if message was sent via hook
    ///</remarks>
    public string hook_key {get; set;}
    ///<remarks>
    /// number of previous visible message
    ///</remarks>
    public BigInteger prev_message_nr {get; set;}
    ///<remarks>
    /// start new sheet in message flow
    ///</remarks>
    public bool is_new_sheet {get; set;}
    }
}
