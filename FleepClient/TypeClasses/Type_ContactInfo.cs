using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{
    /*
     
ContactInfo:

mk_rec_type         text = 'contact'
account_id          text            - Account ID
email               text (o)        - Account email
display_name        text (o)        - name to be displayed in conversations
mk_account_status   text            - See Classificators
is_hidden_for_add   boolean (o)     - can this contact be shown when adding contacts
mk_email_interval   text (o)        - filled only if requester account_id matches
avatar_urls         text (o)        - json contains urls to avatar thumbnails
contact_name        text (o)        - Contact name visible only to contactlist owner
phone_nr            text (o)        - Phone phone nr visible only to contactlist owner
client_flags        text (o)        - List of flag names that are true for client
is_full_privacy     boolean (o)     - true if don't want to send and receive
                                      conversation read level and writing status
activity_time       bigint (o)      - when user send or read something in fleep
dialog_id           uuid (o)        - id of the latest dialog with this user
is_dialog_listed    boolean (o)     - should this contact be displayed as conversation
                                      placeholder in conversation list
alias_account_ids   text[] (o)      - for owner account only list of it's alias account ids
trial_end_time      integer (o)     - time whne trial ends, 0 for premium users
     
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class ContactInfo : FleepTypeBase
    {    
        ///<remarks>
        ///</remarks>
        public string mk_rec_type {get; set;}
        ///<remarks>
        /// Account ID
        ///</remarks>
        public string account_id {get; set;}
        ///<remarks>
        /// Account email
        ///</remarks>
        public string email {get; set;}
        ///<remarks>
        /// name to be displayed in conversations
        ///</remarks>
        public string display_name {get; set;}
        ///<remarks>
        /// See Classificators
        ///</remarks>
        public string mk_account_status {get; set;}
        ///<remarks>
        /// can this contact be shown when adding contacts
        ///</remarks>
        public bool is_hidden_for_add {get; set;}
        ///<remarks>
        /// filled only if requester account_id matches
        ///</remarks>
        public string mk_email_interval {get; set;}
        ///<remarks>
        /// json contains urls to avatar thumbnails
        ///</remarks>
        public string avatar_urls {get; set;}
        ///<remarks>
        /// Contact name visible only to contactlist owner
        ///</remarks>
        public string contact_name {get; set;}
        ///<remarks>
        /// Phone phone nr visible only to contactlist owner
        ///</remarks>
        public string phone_nr {get; set;}
        ///<remarks>
        /// List of flag names that are true for client
        ///</remarks>
        public string client_flags {get; set;}
        ///<remarks>
        /// true if don't want to send and receive
        /// conversation read level and writing status
        ///</remarks>
        public bool is_full_privacy {get; set;}
        ///<remarks>
        /// when user send or read something in fleep
        ///</remarks>
        public BigInteger activity_time {get; set;}
        ///<remarks>
        /// id of the latest dialog with this user
        ///</remarks>
        public string dialog_id {get; set;}
        ///<remarks>
        /// should this contact be displayed as conversation
        /// placeholder in conversation list
        ///</remarks>
        public bool is_dialog_listed {get; set;}
        ///<remarks>
        /// for owner account only list of it's alias account ids
        ///</remarks>
        public List<String> alias_account_ids {get; set;}
        ///<remarks>
        /// time whne trial ends, 0 for premium users
        ///</remarks>
        public int trial_end_time {get; set;}
    }
}
