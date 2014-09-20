using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{


    /*
   ActivityInfo:

    mk_rec_type         text = 'activity'
    conversation_id     text            - Conversation
    account_id          text            - Who is writing
    is_writing          boolean (o)     - true - writing, false - cancel
    message_nr          bigint (o)      - message being edited
    read_message_nr     bigint (o)      - read horizon for this user
     
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class ActivityInfo : FleepTypeBase
    {
    ///<remarks>
    ///</remarks>
    public string mk_rec_type {get; set;}
    ///<remarks>
    /// Conversation
    ///</remarks>
    public string conversation_id {get; set;}
    ///<remarks>
    /// Who is writing
    ///</remarks>
    public string account_id {get; set;}
    ///<remarks>
    /// true - writing, false - cancel
    ///</remarks>
    public bool is_writing {get; set;}
    ///<remarks>
    /// message being edited
    ///</remarks>
    public BigInteger message_nr {get; set;}
    ///<remarks>
    /// read horizon for this user
    ///</remarks>
    public BigInteger read_message_nr {get; set;}
    }
}
