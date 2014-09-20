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
using System.Text.RegularExpressions;


namespace Fleep.TypeClasses
{    
    [JsonObject(MemberSerialization.OptOut)]
    public class DataSync : FleepFunctionalTypeBase
    {
        #region Declarations

        private Account account;

        private Account_PollResponse lastAccountPollResponse;
     
        #endregion

        #region Constructors

        public DataSync()
        {
            this.Initialize();
        }

        public DataSync(Account account)
        {
            this.Initialize(account);
        }

        #endregion

        #region Initializtion Functions

        private void Initialize()
        {
            //Empty initializer
        }

        private void Initialize(Account account)
        {
            this.Account = account;
        }

        #endregion

        #region Properties

        public Account Account
        {
            get
            {
                return this.account;
            }

            set
            {
                if (value != null)
                {
                    this.account = value;
                }
            }
        }

        public Account_PollResponse LastAccountPollResponse
        {
            get { return this.lastAccountPollResponse; }
        }

        #endregion

        #region Methods

      
        #endregion

        #region Utility Functions

        #endregion

    }
}
