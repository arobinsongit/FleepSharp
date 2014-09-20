using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{
/*
     /*
     Output:

    event_horizon       bigint          - event horizon for next poll
    stream              list            - ConvInfo, ContactInfo, MessageInfo, ActivityInfo, HookInfo
    sync_progress       float           - indicates progess of sync from 0.0 to 1.0
    static_version      text            - javascript version. Client should reload when changes
    limit_time          bigint          - time limit applied to non premium users, 0 for premium users
     */

[JsonObject(MemberSerialization.OptOut)]
public class AccountPollOutputSteam : FleepTypeBase
    {
    public List<ConvInfo> ConvInfo { get; set; }
    public List<ContactInfo> ContactInfo { get; set; }
    public List<MessageInfo> MessageInfo { get; set; }
    public List<ActivityInfo> ActivityInfo { get; set; }
    public List<HookInfo> HookInfo { get; set; }    
    }
}
