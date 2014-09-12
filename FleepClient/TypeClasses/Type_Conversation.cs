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
        
        private FleepClient fleepClient;

        private Message_SendResponse lastMessageSendResponse;
        private Conversation_CreateResponse lastConversationCreateResponse;
        private Conversation_SyncBackwardResponse lastConversationSyncBackwardResponse;

        private BigInteger lastMessageNrFromSync = -1;
        
        #endregion

        #region Constructors

        public Conversation()
        {
            this.Initialize();
        }

        public Conversation(FleepClient client)
        {
            this.Initialize(client);
        }

        public Conversation(FleepClient client, string topic, string emails, string message = "",bool is_invite = false)
        {
            this.Initialize(client, topic, emails, message, is_invite);
        }

#endregion

        #region Initializtion Functions

        private void Initialize()
        {
            //Empty initializer
        }

        private void Initialize(FleepClient client)
        {
            this.fleepClient = client;
            this.Initialize();
        }

        private void Initialize(FleepClient client, string topic, string emails, string message = "", bool is_invite = false)
        {         
            this.fleepClient = client;
            this.ConversationCreate(topic, emails, message, is_invite);
            this.Initialize();
        }

        #endregion

        #region Properties
        
        public string ConversationID { get; private set; }
        public Conversation_CreateResponse LastConversationCreateResponse { get { return this.lastConversationCreateResponse; }}
        public Message_SendResponse LastMessageSendResponse { get { return this.lastMessageSendResponse; }}        
        public int LastMessageNumberSent { get { return lastMessageSendResponse.result_message_nr; } }
        public Conversation_SyncBackwardResponse LastConversationSyncBackwardResponse { get { return this.lastConversationSyncBackwardResponse; }}
        
        #endregion

        #region Methods

        public void ConversationCreate(string topic, string emails, string message = "", bool is_invite = false)
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
                this.lastConversationCreateResponse = FleepAPI.CallAPI<Conversation_CreateResponse>(fleepClient, conversationCreateRequest.MethodPath, conversationCreateRequest.ToJSON());

                //Capture the Conversation ID
                this.ConversationID = this.lastConversationCreateResponse.header.conversation_id;
            }

            catch
            {
                // Throw the exception
                throw;
            }
        }

        public void MessageSend(string message)
        {            
            try
            {
                Message_SendRequest messageSendRequest = new Message_SendRequest();

                // Set parameters on the request
                messageSendRequest.ConversationID = this.ConversationID;
                messageSendRequest.message = message;
                
                // Call the API                  
                this.lastMessageSendResponse = FleepAPI.CallAPI<Message_SendResponse>(fleepClient, messageSendRequest.MethodPath, messageSendRequest.ToJSON());               
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        public void SyncBackward(BigInteger from_message_nr)
        {
            try
            {
                Conversation_SyncBackwardRequest conversationSyncBackwardRequest = new Conversation_SyncBackwardRequest();

                // Set parameters on the request
                conversationSyncBackwardRequest.ConversationID = this.ConversationID;
                conversationSyncBackwardRequest.from_message_nr = from_message_nr;

                // Call the API
                this.lastConversationSyncBackwardResponse = FleepAPI.CallAPI<Conversation_SyncBackwardResponse>(fleepClient, conversationSyncBackwardRequest.MethodPath, conversationSyncBackwardRequest.ToJSON());
            }
            catch
            {
                // Throw the exception
                throw;
            }
        }

        #endregion

        #region Utility Functions

        #endregion

    }
}
