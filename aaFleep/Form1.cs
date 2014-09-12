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
using Fleep;
using Fleep.MethodClasses;
using Fleep.TypeClasses;
using Fleep.Exceptions;

using Newtonsoft.Json;
using System.Numerics;

namespace aaFleep
{
    public partial class Form1 : Form
    {

        private Fleep.FleepClient fc;
        private string ConversationID;
        Conversation newConversation;

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
                fc = new Fleep.FleepClient("andy@phase2automation.com", "3EYrAJrPymCFzsdA");

            newConversation = new Conversation(fc);

            newConversation.ConversationCreate("ABC", "aprobi@gmail.com", "First Message 2");

            log("Created Conversation " + newConversation.ConversationID);



            //Message_SendResponse messageSendResponse;

            //newConversation.MessageSend(out messageSendResponse, ConversationID, "Message at " + System.DateTime.Now.ToString());
            //log("Message Number = " + messageSendResponse.result_message_nr);

            //newConversation.MessageSend(out messageSendResponse, ConversationID, "Another Message at " + System.DateTime.Now.Ticks.ToString());
            //log("Message Number = " + messageSendResponse.result_message_nr);

            }
            catch (Exception ex)
            {
                log(ex.ToString());
                return;
            }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            newConversation.MessageSend(textBox1.Text);

            log("Last Message Sent = " + newConversation.LastMessageNumberSent);

            //log("Last Sync Backwards");
            //newConversation.SyncBackward((BigInteger)1);
            //log(newConversation.LastConversationSyncBackwardResponse.ToJSONPrint());


        }

    }
}
