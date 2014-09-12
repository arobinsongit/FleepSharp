using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using FleepClient;
using FleepClient.MethodClasses;
using FleepClient.TypeClasses;
using FleepClient.Exceptions;

using Newtonsoft.Json;
using System.Numerics;

namespace aaFleep
{
    public partial class Form1 : Form
    {

        private FleepClient.Client fc;
        private string ConversationID;

        public string GetPrettyPrintedJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        private void log(string val)
        {
            txtLog.AppendText(Environment.NewLine + System.DateTime.Now.ToString() + "\t" + val);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                fc = new FleepClient.Client("andy@phase2automation.com", "3EYrAJrPymCFzsdA");
            }
            catch (Exception ex)
            {
                log(ex.ToString());
                return;
            }

            Conversation newConversation = new Conversation(fc);

            newConversation.ConversationCreate("ABC", "aprobi@gmail.com", "First Message 2");

            log("Created Conversation " + newConversation.ConversationID);

            newConversation.MessageSend("This is a New Message Send");
            newConversation.MessageSend("This is a New Message Send");
            newConversation.MessageSend("This is a New Message Send");

            newConversation.SyncBackward((BigInteger)0);

            int x = 1;

            newConversation.SyncBackward((BigInteger)9999);

            x = 2;

            //Message_SendResponse messageSendResponse;

            //newConversation.MessageSend(out messageSendResponse, ConversationID, "Message at " + System.DateTime.Now.ToString());
            //log("Message Number = " + messageSendResponse.result_message_nr);

            //newConversation.MessageSend(out messageSendResponse, ConversationID, "Another Message at " + System.DateTime.Now.Ticks.ToString());
            //log("Message Number = " + messageSendResponse.result_message_nr);

            

        }
    }
}
