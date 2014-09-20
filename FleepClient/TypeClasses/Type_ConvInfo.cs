using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;

namespace Fleep.TypeClasses
{


    /*
     
      ConvInfo:

        mk_rec_type         text = 'conv'
        conversation_id     text            - mostly needed for conversation create case
        join_message_nr     bigint          - message_nr when member was added to the conversation
                                              Useful for determining if backward scroll is still possible
        read_message_nr     bigint          - Current truth about read horizon. Normally it only
                                              increased but sometimes user may mark some messages
                                              unread.
        show_message_nr     bigint          - number of latest visible message
        last_message_nr     bigint          - last message nr in the conversation or last message
                                              that is shown in case of leaving conversation
        inbox_message_nr    bigint          - point to the message to be displayed in conversation list
        can_post            boolean         - true if profile is still in conevrsation
                                              and can post to this conversation
        topic               text            - topic. Returned when changed
        members             text[]          - list of all member id's who are currently joined
        leavers             text[]          - list of all member id's who have left conversation
        last_message_time   double          - unix timestamp of last message in flow
        mk_alert_level      text            - should alerting from this conversation be supressed
        file_horizon        bigint          - used in case all files are not sent during init
                                              pass it to api/conversation/sync_files call as
                                              from_message_nr
        pin_horizon         bigint          - used in case all pins are not sent during init
                                              pass it to api/conversation/sync_pinboard call as
                                              from_message_nr
        hide_message_nr     bigint          - if conversation last message nr is equal or smaller than
                                              hide message nr then conversation is to be hidden in client
        fw_message_nr       bigint          - when syncing conversation forward mesage nr shows
                                              forward progress and all is synced when it equals last_message_nr
        bw_message_nr       bigint          - when syncing conversation backward message nr shows
                                              backward progress and all is synced when it equals join_message_nr
        unread_count        bigint          - number of unread messages in conversation
        last_inbox_nr       bigint          - inbox number of last alerting inbox message. Can be used locally
                                              to reduce unread count while server asyncronously does the same
        is_deleted          boolean         - If set to true by server conversation must be deleted from
                                              client cache.
        is_tiny             boolean         - stream contains minimal amount of information needed for displaying
                                              conversation in list
        is_init             boolean         - server requests cache reset for this conversation. Used for changes
                                              like disclose and rejoin
        teams               text[]          - list of team id's that are part of this conversation
        cmail               text            - conversation email address, sending email to this address
                                              posts message in the conversation
        has_skipped_content boolean         - This conversation or search result has content that is behind
                                              visibility horizon fro free users.
     
     */

    [JsonObject(MemberSerialization.OptOut)]
    public class ConvInfo : FleepTypeBase
    {
        ///<remarks>
        ///</remarks>
        public string mk_rec_type {get; set;}
        ///<remarks>
        /// mostly needed for conversation create case
        ///</remarks>
        public string conversation_id {get; set;}
        ///<remarks>
        /// message_nr when member was added to the conversation
        /// Useful for determining if backward scroll is still possible
        ///</remarks>
        public BigInteger join_message_nr {get; set;}
        ///<remarks>
        /// Current truth about read horizon. Normally it only
        /// increased but sometimes user may mark some messages
        /// unread.
        ///</remarks>
        public BigInteger read_message_nr {get; set;}
        ///<remarks>
        /// number of latest visible message
        ///</remarks>
        public BigInteger show_message_nr {get; set;}
        ///<remarks>
        /// last message nr in the conversation or last message
        /// that is shown in case of leaving conversation
        ///</remarks>
        public BigInteger last_message_nr {get; set;}
        ///<remarks>
        /// point to the message to be displayed in conversation list
        ///</remarks>
        public BigInteger inbox_message_nr {get; set;}
        ///<remarks>
        /// true if profile is still in conevrsation
        /// and can post to this conversation
        ///</remarks>
        public bool can_post {get; set;}
        ///<remarks>
        /// topic. Returned when changed
        ///</remarks>
        public string topic {get; set;}
        ///<remarks>
        /// list of all member id's who are currently joined
        ///</remarks>
        public List<String> members {get; set;}
        ///<remarks>
        /// list of all member id's who have left conversation
        ///</remarks>
        public List<String> leavers {get; set;}
        ///<remarks>
        /// unix timestamp of last message in flow
        ///</remarks>
        public double last_message_time {get; set;}
        ///<remarks>
        /// should alerting from this conversation be supressed
        ///</remarks>
        public string mk_alert_level {get; set;}
        ///<remarks>
        /// used in case all files are not sent during init
        /// pass it to api/conversation/sync_files call as
        /// from_message_nr
        ///</remarks>
        public BigInteger file_horizon {get; set;}
        ///<remarks>
        /// used in case all pins are not sent during init
        /// pass it to api/conversation/sync_pinboard call as
        /// from_message_nr
        ///</remarks>
        public BigInteger pin_horizon {get; set;}
        ///<remarks>
        /// if conversation last message nr is equal or smaller than
        /// hide message nr then conversation is to be hidden in client
        ///</remarks>
        public BigInteger hide_message_nr {get; set;}
        ///<remarks>
        /// when syncing conversation forward mesage nr shows
        /// forward progress and all is synced when it equals last_message_nr
        ///</remarks>
        public BigInteger fw_message_nr {get; set;}
        ///<remarks>
        /// when syncing conversation backward message nr shows
        ///</remarks>
        public BigInteger bw_message_nr {get; set;}
        ///<remarks>
        /// number of unread messages in conversation
        ///</remarks>
        public BigInteger unread_count {get; set;}
        ///<remarks>
        /// inbox number of last alerting inbox message. Can be used locally
        ///</remarks>
        public BigInteger last_inbox_nr {get; set;}
        ///<remarks>
        /// If set to true by server conversation must be deleted from
        ///</remarks>
        public bool is_deleted {get; set;}
        ///<remarks>
        /// stream contains minimal amount of information needed for displaying
        ///</remarks>
        public bool is_tiny {get; set;}
        ///<remarks>
        /// server requests cache reset for this conversation. Used for changes
        ///</remarks>
        public bool is_init {get; set;}
        ///<remarks>
        /// list of team id's that are part of this conversation
        ///</remarks>
        public List<String> teams {get; set;}
        ///<remarks>
        /// conversation email address, sending email to this address
        ///</remarks>
        public string cmail {get; set;}
        ///<remarks>
        /// This conversation or search result has content that is behind
        ///</remarks>
        public bool has_skipped_content {get; set;}
    }
}
