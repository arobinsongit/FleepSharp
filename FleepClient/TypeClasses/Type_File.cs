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
        
        private Account account;
        private File_UploadResponse lastFileUploadResponse;
        
        #endregion

        #region Constructors

        public File()
        {
            this.Initialize();
        }

        public File(Account account)
        {
            this.Initialize(account);
        }

        public File(Account account, string topic, string emails, string message = "", bool is_invite = false)
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
            //this.ConversationCreate(topic, emails, message, is_invite);
            this.Initialize();
        }

        #endregion

        #region Properties
        
        public string ConversationID { get; set; }
        
        #endregion

        #region Methods

        public File_UploadResponse FileUpload(string filepath, string conversationID)
        {
            try
            {
                File_UploadRequest fileUploadRequest = new File_UploadRequest();
                FileInfoList fileInfoList = new FileInfoList();

                // Set parameters on the request
                fileUploadRequest.filepath = filepath;
                fileUploadRequest.ConversationID = conversationID;                

                // Make the API CALL
                return FleepAPI.UploadFile(this.account, fileUploadRequest);

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
