using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{
    /*
TeamInfo:

mk_rec_type         text = 'team'
team_id             text            - Id for team used in conversation heder to map all the teams
                                      that are part of the conversation
team_name           text            - Name of the team
members             text[]          - List of account_id's who are part of the team
conversations       text[]          - List of conversations team is participation in 
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class TeamInfo : FleepTypeBase
    {
    ///<remarks>
    ///</remarks>
    public string mk_rec_type {get; set;}
    ///<remarks>
    /// Id for team used in conversation heder to map all the teams
    /// that are part of the conversation
    ///</remarks>
    public string team_id {get; set;}
    ///<remarks>
    /// Name of the team
    ///</remarks>
    public string team_name {get; set;}
    ///<remarks>
    /// List of account_id's who are part of the team
    ///</remarks>
    public List<String> members {get; set;}
    ///<remarks>
    /// List of conversations team is participation in
    ///</remarks>
    public List<String> conversations {get; set;}
    }
}
