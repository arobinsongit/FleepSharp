using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using Fleep.TypeClasses;
using Fleep.Exceptions;
using System.Text.RegularExpressions;
using System.Numerics;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Account_PollResponse : MethodResponseBase
    {
        /*
         account/poll
         URL: https://fleep.io/api/account/poll

         Handles long poll server side by storing connection information into connection table by the token.

         Input:

         event_horizon       bigint = 0      - latest event client has seen
         wait                boolean = True  - Set to False if you want to get latest events
                                               but not stay waiting if there isn't any
         poll_flags          text[] = null   - Can be used to fine tune polls
                                               according to client needs.
                                               skip_hidden - skip hidden conversations
                                               during initial sync.
                                               skip_rest - skip other conversations and start polling events
                                               used for limiting number of conversations loaded into client cache
         Output:

         event_horizon       bigint          - event horizon for next poll
         stream              list            - ConvInfo, ContactInfo, MessageInfo, ActivityInfo, HookInfo
         sync_progress       float           - indicates progess of sync from 0.0 to 1.0
         static_version      text            - javascript version. Client should reload when changes
         limit_time          bigint          - time limit applied to non premium users, 0 for premium users
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
         ActivityInfo:

         mk_rec_type         text = 'activity'
         conversation_id     text            - Conversation
         account_id          text            - Who is writing
         is_writing          boolean (o)     - true - writing, false - cancel
         message_nr          bigint (o)      - message being edited
         read_message_nr     bigint (o)      - read horizon for this user
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
         RequestInfo:

         mk_rec_type         text = 'request'
         client_req_id       uuid            - Client request has been processed and all data changes
                                               are commited into database and they have all been
                                               sent through poll also
         result_message_nr   bigint          - Message that was created or modified with this request
         TeamInfo:

         mk_rec_type         text = 'team'
         team_id             text            - Id for team used in conversation heder to map all the teams
                                               that are part of the conversation
         team_name           text            - Name of the team
         members             text[]          - List of account_id's who are part of the team
         conversations       text[]          - List of conversations team is participation in
         */

        #region Outputs

        ///<remarks>
        /// event horizon for next poll
        ///</remarks>
        BigInteger event_horizon { get; set; }
        ///<remarks>
        AccountPollOutputSteam stream { get; set; }
        ///<remarks>
        /// indicates progess of sync from 0.0 to 1.0
        ///</remarks>
        float sync_progress { get; set; }
        /// javascript version. Client should reload when changes
        ///</remarks>
        string static_version { get; set; }
        ///<remarks>
        /// time limit applied to non premium users, 0 for premium users
        ///</remarks>
        BigInteger limit_time { get; set; }
        
        #endregion

        
        #region Constructors

        // No constructors

        #endregion

        #region JSON Methods

        #endregion


    }
}
