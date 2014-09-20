using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{


    /*
    HookInfo:

    mk_rec_type         text = 'hook'
    conversation_id     uuid            - Conversation
    account_id          uuid            - Creator of the hook
    hook_name           text            - Hooks display name in chat. If not provided
                                          owners display name is used
    hook_key            text            - Used for removing th hook
    hook_url            text            - Url that can be used to send data into chat
                                          filled only for the owner
    is_active           boolean         - is this hook currently active or for
                                          historical data
    mk_hook_type        text            - plain, github, jira
    avatar_urls         text            - Avatar displayed for hook messages
     
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class HookInfo : FleepTypeBase
    {
        ///<remarks>
        ///</remarks>
        public string mk_rec_type {get; set;}
        ///<remarks>
        /// Conversation
        ///</remarks>
        public string conversation_id {get; set;}
        ///<remarks>
        /// Creator of the hook
        ///</remarks>
        public string account_id {get; set;}
        ///<remarks>
        /// Hooks display name in chat. If not provided
        /// owners display name is used
        ///</remarks>
        public string hook_name {get; set;}
        ///<remarks>
        /// Used for removing th hook
        ///</remarks>
        public string hook_key {get; set;}
        ///<remarks>
        /// Url that can be used to send data into chat
        /// filled only for the owner
        ///</remarks>
        public string hook_url {get; set;}
        ///<remarks>
        /// is this hook currently active or for
        /// historical data
        ///</remarks>
        public bool is_active {get; set;}
        ///<remarks>
        /// plain, github, jira
        ///</remarks>
        public string mk_hook_type {get; set;}
        ///<remarks>
        /// Avatar displayed for hook messages
        ///</remarks>
        public string avatar_urls {get; set;}
    }
}
