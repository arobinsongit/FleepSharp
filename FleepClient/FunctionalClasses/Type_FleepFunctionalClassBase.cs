using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using System.Text;

namespace Fleep.TypeClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FleepFunctionalTypeBase : FleepTypeBase
    {
        #region Declarations
        
        private string apiURL = "https://fleep.io/api/";
        
        #endregion

        #region Properties

        public string ApiURL
        {
            get
            {
                return apiURL;
            }

            set
            {
                if (value != "")
                {
                    this.apiURL = value;
                }
            }
        }
        #endregion


        #region JSON Methods

        #endregion

    }
}
