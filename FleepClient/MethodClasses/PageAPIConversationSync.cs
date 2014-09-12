using System;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Collections.Generic;
using Fleep.TypeClasses;
using Fleep.Exceptions;

namespace Fleep.MethodClasses
{
    [JsonObject(MemberSerialization.OptOut)]
    public class PageAPIConversationSync : MethodResponseBase
    {
        #region "Outputs"

        public PageAPIConversationSyncHeader header { get; set; }
        public List<PageAPIConversationSyncStream> stream { get; set; }

        #endregion

        #region "Constructors"

        // No constructors

        #endregion

        #region "JSON Methods"

        #endregion

    }
}
