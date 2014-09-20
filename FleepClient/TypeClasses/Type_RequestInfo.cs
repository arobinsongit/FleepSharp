using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{
    /*
RequestInfo:

mk_rec_type         text = 'request'
client_req_id       uuid            - Client request has been processed and all data changes
                                      are commited into database and they have all been
                                      sent through poll also
result_message_nr   bigint          - Message that was created or modified with this request     
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class RequestInfo : FleepTypeBase
    {
    ///<remarks>
    ///</remarks>
    public string mk_rec_type {get; set;}
    ///<remarks>
    /// Client request has been processed and all data changes
    /// are commited into database and they have all been
    /// sent through poll also
    ///</remarks>
    public string client_req_id {get; set;}
    ///<remarks>
    /// Message that was created or modified with this request
    ///</remarks>
    public BigInteger result_message_nr {get; set;}
    }
}
