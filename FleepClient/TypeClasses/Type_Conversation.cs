using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;
using Fleep.MethodClasses;
using Fleep.TypeClasses;
using Fleep.Exceptions;
using Fleep.UtilityMethods;
using System.Net;


namespace Fleep.TypeClasses
{
    // Reference : http://json2csharp.com/#

    [JsonObject(MemberSerialization.OptOut)]
    public class Conversation : FleepTypeBase
    {

        #region Declarations

        private Account account;

        private Message_SendResponse lastMessageSendResponse;
        private Conversation_CreateResponse lastConversationCreateResponse;
        private Conversation_DeleteResponse lastConversationDeleteResponse;
        private Conversation_SyncBackwardResponse lastConversationSyncBackwardResponse;
        private Conversation_SyncResponse lastConversationSyncResponse;

        private BigInteger lastMessageNrFromSync = -1;
        
        #endregion

        #region Constructors

        public Conversation()
        {
            this.Initialize();
        }

        public Conversation(Account account)
        {
            this.Initialize(account);
        }

        public Conversation(Account account, string topic, string emails, string message = "",bool is_invite = false)
        {
            this.Initialize(account, topic, emails, message, is_invite);
        }

#endregion

        #region Initializtion Functions

        private void Initialize()
        {
            //Empty initializer
        }

        private void Initialize(Account account)
        {
            this.account = account;
            this.Initialize();
        }

        private void Initialize(Account account, string topic, string emails, string message = "", bool is_invite = false)
        {         
            this.account = account;
            this.ConversationCreate(topic, emails, message, is_invite);
            this.Initialize();
        }

        #endregion

        #region Properties
        
        public string ConversationID { get; private set; }
        
        public BigInteger LastMessageNumberSent 
        { 
            get {return (lastMessageSendResponse != null) ? lastMessageSendResponse.result_message_nr : -1;} 
        }

        public BigInteger LastMessageNrFromSync
        { 
            get { return (lastConversationSyncResponse != null) ? lastConversationSyncResponse.header.last_message_nr : -1; } 
        }

        public Conversation_CreateResponse LastConversationCreateResponse 
        {
            get { return this.lastConversationCreateResponse; } 
        }

        public Message_SendResponse LastMessageSendResponse 
        { 
            get { return this.lastMessageSendResponse; } 
        }                

        public Conversation_SyncBackwardResponse LastConversationSyncBackwardResponse 
        { 
            get { return this.lastConversationSyncBackwardResponse; }
        }
        
        public Conversation_SyncResponse LastConversationSyncResponse
        { 
            get { return this.lastConversationSyncResponse; } 
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Allow a caller to instantiate a new conversation then explicity subscribe to a CONV ID that they know from a different method
        /// </summary>
        /// <param name="conversationID"></param>
        public void Subscribe(string conversationID)
        {
            this.ConversationID = conversationID;
        }

        public Conversation_CreateResponse ConversationCreate(string topic, string emails, string message = "", bool is_invite = false)
        {
            try
            {
                Conversation_CreateRequest conversationCreateRequest = new Conversation_CreateRequest();

                // Set parameters on the request
                conversationCreateRequest.topic = topic;
                conversationCreateRequest.emails = emails;
                conversationCreateRequest.message = message;
                conversationCreateRequest.is_invite = is_invite;
                //TODO conversationCreateRequest.files = files;

                // Make the API CALL
                this.lastConversationCreateResponse = FleepAPI.CallAPI<Conversation_CreateResponse>(account, conversationCreateRequest.MethodPath, conversationCreateRequest.ToJSON());

                //Capture the Conversation ID
                this.ConversationID = this.lastConversationCreateResponse.header.conversation_id;

                // Return Results
                return this.lastConversationCreateResponse;
            }

            catch
            {
                // Throw the exception
                throw;
            }
        }

        public Conversation_DeleteResponse ConversationDelete()
        {
            try
            {
                Conversation_DeleteRequest conversationDeleteRequest = new Conversation_DeleteRequest();

                // Set parameters on the request
                conversationDeleteRequest.ConversationID = this.ConversationID;

                // Make the API CALL
                this.lastConversationDeleteResponse = FleepAPI.CallAPI<Conversation_DeleteResponse>(account, conversationDeleteRequest.MethodPath, conversationDeleteRequest.ToJSON());

                // Return Results
                return this.lastConversationDeleteResponse;
            }

            catch
            {
                // Throw the exception
                throw;
            }
        }

        /// <summary>
        /// Static call for Conversation Delete
        /// </summary>
        /// <param name="fleepClient"></param>
        /// <param name="conversationID"></param>
        /// <returns></returns>
        public static Conversation_DeleteResponse ConversationDelete(Account account, string conversationID)
        {
            try
            {
                Conversation_DeleteRequest conversationDeleteRequest = new Conversation_DeleteRequest();

                // Set parameters on the request
                conversationDeleteRequest.ConversationID = conversationID;

                // Make the API CALL
                return FleepAPI.CallAPI<Conversation_DeleteResponse>(account, conversationDeleteRequest.MethodPath, conversationDeleteRequest.ToJSON());
            }

            catch
            {
                // Throw the exception
                throw;
            }
        }


        #region MessageSend
        
        public Message_SendResponse MessageSend(Message_SendRequest messageSendRequest)
        {
            try
            {
                // Call the API                  
                this.lastMessageSendResponse = FleepAPI.CallAPI<Message_SendResponse>(account, messageSendRequest.MethodPath, messageSendRequest.ToJSON());

                // Return Result
                return this.lastMessageSendResponse;
            }
            catch
            {
                // Throw the exception
                throw;
            }

        }

        public Message_SendResponse MessageSend(string message, BigInteger from_message_nr = new BigInteger())
        {
            try
            {
                Message_SendRequest messageSendRequest = new Message_SendRequest();

                // Set parameters on the request
                messageSendRequest.ConversationID = this.ConversationID;
                messageSendRequest.message = message;

                if (from_message_nr > 0)
                {
                    messageSendRequest.from_message_nr = from_message_nr;
                }

                // Call internal function
                return this.MessageSend(messageSendRequest);
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        public Message_SendResponse MessageSend(string message, List<string> file_ids, BigInteger from_message_nr = new BigInteger())
        {
            try
            {
                Message_SendRequest messageSendRequest = new Message_SendRequest();

                // Set parameters on the request
                messageSendRequest.ConversationID = this.ConversationID;
                messageSendRequest.message = message;
                messageSendRequest.file_ids = file_ids;

                if(from_message_nr > 0)
                {
                    messageSendRequest.from_message_nr = from_message_nr;
                }


                // Call internal function
                return this.MessageSend(messageSendRequest);
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        public Message_SendResponse MessageSend(string message, FileInfoList files, BigInteger from_message_nr = new BigInteger())
        {
            try
            {
                Message_SendRequest messageSendRequest = new Message_SendRequest();

                // Set parameters on the request
                messageSendRequest.ConversationID = this.ConversationID;
                messageSendRequest.message = message;
                messageSendRequest.files = files;

                if (from_message_nr > 0)
                {
                    messageSendRequest.from_message_nr = from_message_nr;
                }

                // Call internal function
                return this.MessageSend(messageSendRequest);
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        #endregion


        public Conversation_SyncBackwardResponse SyncBackward(BigInteger from_message_nr)
        {
            try
            {
                Conversation_SyncBackwardRequest conversationSyncBackwardRequest = new Conversation_SyncBackwardRequest();

                // Set parameters on the request
                conversationSyncBackwardRequest.ConversationID = this.ConversationID;
                conversationSyncBackwardRequest.from_message_nr = from_message_nr;

                // Call the API
                this.lastConversationSyncBackwardResponse = FleepAPI.CallAPI<Conversation_SyncBackwardResponse>(account, conversationSyncBackwardRequest.MethodPath, conversationSyncBackwardRequest.ToJSON());

                // Return Result
                return this.lastConversationSyncBackwardResponse;
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        public Conversation_SyncResponse Sync()
        {
            // Sync all Messages
            return this.Sync(0);
        }

        public Conversation_SyncResponse Sync(BigInteger from_message_nr, string mkDirection = "")
        {
            
            try
            {
                Conversation_SyncRequest conversationSyncRequest = new Conversation_SyncRequest();

                // Set parameters on the request
                conversationSyncRequest.ConversationID = this.ConversationID;
                conversationSyncRequest.from_message_nr = from_message_nr;
                conversationSyncRequest.mk_direction = mkDirection;

                // Call the API
                this.lastConversationSyncResponse = FleepAPI.CallAPI<Conversation_SyncResponse>(account, conversationSyncRequest.MethodPath, conversationSyncRequest.ToJSON());
                
                // Return Results
                return this.lastConversationSyncResponse;
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        public Conversation_SyncResponse SyncSinceLast()
        {
            // Sync all messages since the last message we know we read
            return this.Sync(this.LastMessageNrFromSync);
        }

        #endregion

        #region Utility Functions

        #endregion

    }
}
