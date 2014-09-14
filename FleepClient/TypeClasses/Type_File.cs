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
    [JsonObject(MemberSerialization.OptOut)]
    public class File : FleepTypeBase
    {
        #region Declarations
        
        private FleepClient fleepClient;

        private File_UploadResponse lastFileUploadResponse;

        
        #endregion

        #region Constructors

        public File()
        {
            this.Initialize();
        }

        public File(FleepClient client)
        {
            this.Initialize(client);
        }

        public File(FleepClient client, string topic, string emails, string message = "", bool is_invite = false)
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
            //this.ConversationCreate(topic, emails, message, is_invite);
            this.Initialize();
        }

        #endregion

        #region Properties
        
        public string ConversationID { get; set; }
        
        #endregion

        #region Methods

        public FileInfoList FileUpload(string filepath, string conversationID)
        {
            try
            {
                File_UploadRequest fileUploadRequest = new File_UploadRequest();
                FileInfoList fileInfoList = new FileInfoList();

                // Set parameters on the request
                fileUploadRequest.filepath = filepath;
                fileUploadRequest.ConversationID = conversationID;

                // Make the API CALL
                return FleepAPI.UploadFile(this.fleepClient, fileUploadRequest.MethodPath, fileUploadRequest);

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
