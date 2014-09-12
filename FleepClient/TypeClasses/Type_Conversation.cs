using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Numerics;
using FleepClient.MethodClasses;
using FleepClient.TypeClasses;
using FleepClient.Exceptions;
using FleepClient.UtilityMethods;

using System.Net;


namespace FleepClient.TypeClasses
{
    // Reference : http://json2csharp.com/#

    [JsonObject(MemberSerialization.OptOut)]
    public class Conversation : FleepTypeBase
    {
        #region Declarations
        
        private string apiURL;
        
        private Message_SendResponse lastMessageSendResponse;

        private Conversation_CreateResponse lastConversationCreateResponse;
        private Conversation_SyncBackwardResponse lastConversationSyncBackwardResponse;

        #endregion

        #region Constructors

        public Conversation()
        {
            this.Initialize();
        }

        public Conversation(Client client)
        {
            this.Initialize(client);
        }

        public Conversation(string tokenID, string ticket, string apiURL)
        {
            this.Initialize(tokenID, ticket, apiURL);
        }

        public Conversation(string tokenID, string ticket, string apiURL, string topic, string emails, string message = "",bool is_invite = false)
        {

            this.TokenID = tokenID;
            this.Ticket = ticket;
            this.apiURL = apiURL;
        }




        private void Initialize()
        {
            //Empty initializer
        }

        private void Initialize(string tokenID, string ticket, string apiURL)
        {
            this.TokenID = tokenID;
            this.Ticket = ticket;
            this.apiURL = apiURL;
            this.Initialize();
        }

        private void Initialize(Client client)
        {
            this.Initialize(client.TokenID, client.Ticket, client.ApiURL);
        }

        private void Initialize(string tokenID, string ticket, string apiURL, string topic, string emails, string message = "", bool is_invite = false)
        {
            this.Initialize(tokenID,ticket,apiURL);
            this.ConversationCreate(topic, emails, message, is_invite);
        }

        private void Initialize(Client client, string topic, string emails, string message = "", bool is_invite = false)
        {
            this.Initialize(client);
            this.ConversationCreate(topic, emails, message, is_invite);
        }

        #endregion

        #region Properties
        
        public string ConversationID { get; private set; }
        public string TokenID { get; private set; }
        public string Ticket { get; private set; }
        public Conversation_CreateResponse LastConversationCreateResponse { get { return this.lastConversationCreateResponse; }}
        public Message_SendResponse LastMessageSendResponse { get { return this.lastMessageSendResponse; }}        
        public int LastMessageNumberSent { get { return lastMessageSendResponse.result_message_nr; } }
        public Conversation_SyncBackwardResponse LastConversationSyncBackwardResponse { get { return this.lastConversationSyncBackwardResponse; }}
        
        #endregion

        #region methods

        public int ConversationCreate(string topic, string emails, string message = "", bool is_invite = false)
        {
            int returnValue;
            Conversation_CreateRequest conversationCreateRequest;
            Conversation_CreateResponse conversationCreateResponse;

            WebHeaderCollection responseHeaderCollection;

            if (!this.Connected)
            {
                throw new NotLoggedInException();
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    conversationCreateRequest = new Conversation_CreateRequest();

                    // Set parameters on the request
                    conversationCreateRequest.topic = topic;
                    conversationCreateRequest.emails = emails;
                    conversationCreateRequest.message = message;
                    conversationCreateRequest.is_invite = is_invite;
                    //TODO conversationCreateRequest.files = files;

                    conversationCreateRequest.ticket = this.Ticket;

                    // Add the required default headers
                    UtilityMethods.UtilityMethods.AddDefaultHeaders(client, this.TokenID);

                    // Make the call via POST
                    String response = client.UploadString(apiURL + conversationCreateRequest.MethodPath, "POST", conversationCreateRequest.ToJSON());

                    // Save the Login Web Header Response for evaluation in the object
                    responseHeaderCollection = client.ResponseHeaders;

                    // Deserialize the Reponse into a proper object                 
                    conversationCreateResponse = JsonConvert.DeserializeObject<Conversation_CreateResponse>(response.ToString());

                    //Save the last response
                    this.lastConversationCreateResponse = conversationCreateResponse;

                    //Capture the Conversation ID
                    this.ConversationID = conversationCreateResponse.header.conversation_id;

                }

                returnValue = 0;
            }
            catch (Exception ex)
            {
                // Throw the exception
                throw ex;
            }

            finally
            {
                // Do Nothing   
            }

            // Return the result value, should only be 0
            return returnValue;
        }

        public int MessageSend(string message)
        {
            int returnValue;
            Message_SendRequest messageSendRequest;
            Message_SendResponse messageSendResponse;

            WebHeaderCollection responseHeaderCollection;

            if (!this.Connected)
            {
                throw new NotLoggedInException();
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    messageSendRequest = new Message_SendRequest();

                    // Set parameters on the request
                    messageSendRequest.ConversationID = this.ConversationID;
                    messageSendRequest.message = message;
                    messageSendRequest.ticket = this.Ticket;

                    // Add the required default headers
                    UtilityMethods.UtilityMethods.AddDefaultHeaders(client,this.TokenID);

                    // Make the call via POST
                    String response = client.UploadString(apiURL + messageSendRequest.MethodPath, "POST", messageSendRequest.ToJSON());

                    // Save the Login Web Header Response for evaluation in the object
                    responseHeaderCollection = client.ResponseHeaders;

                    // Deserialize the Reponse into a proper object                 
                    messageSendResponse = JsonConvert.DeserializeObject<Message_SendResponse>(response.ToString());

                    //Save the last response
                    this.lastMessageSendResponse = messageSendResponse;

                }

                returnValue = 0;
            }
            catch (Exception ex)
            {
                // Throw the exception
                throw ex;
            }

            finally
            {
                // Do Nothing   
            }

            // Return the result value, should only be 0
            return returnValue;
        }

        public int SyncBackward(BigInteger from_message_nr)
        {
            int returnValue;
            Conversation_SyncBackwardRequest conversationSyncBackwardRequest;
            Conversation_SyncBackwardResponse conversationSyncBackwardResponse;

            WebHeaderCollection responseHeaderCollection;

            if (!this.Connected)
            {
                throw new NotLoggedInException();
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    conversationSyncBackwardRequest = new Conversation_SyncBackwardRequest();

                    // Set parameters on the request
                    conversationSyncBackwardRequest.ConversationID = this.ConversationID;
                    conversationSyncBackwardRequest.from_message_nr = from_message_nr;
                    conversationSyncBackwardRequest.ticket = this.Ticket;

                    // Add the required default headers
                    UtilityMethods.UtilityMethods.AddDefaultHeaders(client, this.TokenID);

                    // Make the call via POST
                    String response = client.UploadString(apiURL + conversationSyncBackwardRequest.MethodPath, "POST", conversationSyncBackwardRequest.ToJSON());

                    // Save the Login Web Header Response for evaluation in the object
                    responseHeaderCollection = client.ResponseHeaders;

                    // Deserialize the Reponse into a proper object                 
                    conversationSyncBackwardResponse = JsonConvert.DeserializeObject<Conversation_SyncBackwardResponse>(response.ToString());

                    //Save the last response
                    this.lastConversationSyncBackwardResponse = conversationSyncBackwardResponse;
                }

                returnValue = 0;
            }
            catch (Exception ex)
            {
                // Throw the exception
                throw ex;
            }

            finally
            {
                // Do Nothing   
            }

            // Return the result value, should only be 0
            return returnValue;
        }

        #endregion

        #region "Utility Functions"

        /// <summary>
        /// Check to see if this instance is connected
        /// </summary>
        public bool Connected
        {
            get
            {
                return ((this.TokenID != "") && (this.Ticket != ""));
            }
        }

        #endregion

    }
}
