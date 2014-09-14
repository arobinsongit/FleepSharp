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

using Microsoft.VisualBasic;

using Microsoft.Win32;
using System.Diagnostics;


namespace aaFleep
{
    public partial class Form1 : Form
    {

        private Fleep.FleepClient fc;
        private string ConversationID;
        Conversation newConversation;

        private List<string> ConvIDList;

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

        private void et(Stopwatch s)
        {
            log("Elapsed : " + s.ElapsedMilliseconds.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Stopwatch s = Stopwatch.StartNew();

            try
            {
            if ((txtTicket.Text != "")&(txtTokenID.Text != ""))
            {
                fc = new FleepClient();
                fc.TokenID = txtTokenID.Text;
                fc.Ticket = txtTicket.Text;                
            }
            else
            {
                fc = new Fleep.FleepClient("andy@phase2automation.com", "3EYrAJrPymCFzsdA");
            }

            txtTicket.Text = fc.Ticket;
            txtTokenID.Text = fc.TokenID;

            log("Connected = " + fc.Connected.ToString());

            txtConvTopic.Text = "Topic " + System.DateTime.Now.ToString();

            et(s);

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
            try
            {
                Stopwatch s = Stopwatch.StartNew();

                newConversation.MessageSend(textBox1.Text);

                log("Last Message Sent = " + newConversation.LastMessageNumberSent);

                et(s);
            }
            catch(Exception ex)
            {
                log(ex.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch s = Stopwatch.StartNew();

                BigInteger LastMsgNr;


                if (BigInteger.TryParse(txtLastMessage.Text,out LastMsgNr))
                {
                    newConversation.Sync(LastMsgNr);
                    txtLog.Text = "";
                    log(newConversation.LastConversationSyncResponse.ToJSONPrint());

                    txtLastMessage.Text = newConversation.LastConversationSyncResponse.header.last_message_nr.ToString();
                }
                else
                {
                    log("Can't Parse");
                }

                et(s);

            }
            catch (Exception ex)
            {
                log(ex.ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch s = Stopwatch.StartNew();

                    newConversation.SyncSinceLast();
                    txtLog.Text = "";
                    log(newConversation.LastConversationSyncResponse.ToJSONPrint());

                    txtLastMessage.Text = newConversation.LastMessageNrFromSync.ToString();
                    et(s);
            }
            catch (Exception ex)
            {
                log(ex.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            openFileDialog1.FileName = txtFilename.Text;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                txtFilename.Text = file;
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            txtTicket.Text = RegistryHelper.GetSetting("Fleep", "Ticket", "");
            txtTokenID.Text = RegistryHelper.GetSetting("Fleep", "TokenID", "");

            ConvIDList = new List<string>();

        }

        private void Form1_Closing(object sender, System.EventArgs e)
        {
            RegistryHelper.SaveSetting("Fleep", "Ticket", txtTicket.Text);
            RegistryHelper.SaveSetting("Fleep", "TokenID", txtTokenID.Text);

            foreach(string ConvID in ConvIDList)
            {
                log("Deleting " + ConvID);
                Conversation.ConversationDelete(fc, ConvID);
                
            }

            int x = 1;
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            Stopwatch s = Stopwatch.StartNew();
            newConversation = new Conversation(fc);
            newConversation.ConversationCreate(txtConvTopic.Text, "aprobi@gmail.com");
            log("Created Conversation " + newConversation.ConversationID);
            txtConvID.Text = newConversation.ConversationID;
            ConvIDList.Add(newConversation.ConversationID);
            et(s);
        }

        #region RegHelper
        public class RegistryHelper
        {
            private static string FormRegKey(string sSect)
            {
                return sSect;
            }
            public static void SaveSetting(string Section, string Key, string Setting)
            {

                string text1 = FormRegKey(Section);
                RegistryKey key1 =

                Application.UserAppDataRegistry.CreateSubKey(text1);
                if (key1 == null)
                {
                    return;
                }
                try
                {
                    key1.SetValue(Key, Setting);
                }
                catch (Exception exception1)
                {
                    return;
                }
                finally
                {
                    key1.Close();
                }

            }
            public static string GetSetting(string Section, string Key, string Default)
            {
                if (Default == null)
                {
                    Default = "";
                }
                string text2 = FormRegKey(Section);
                RegistryKey key1 = Application.UserAppDataRegistry.OpenSubKey(text2);
                if (key1 != null)
                {
                    object obj1 = key1.GetValue(Key, Default);
                    key1.Close();
                    if (obj1 != null)
                    {
                        if (!(obj1 is string))
                        {
                            return null;
                        }
                        return (string)obj1;
                    }
                    return null;
                }
                return Default;
            }
        }

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch s = Stopwatch.StartNew();
                Fleep.TypeClasses.File f = new Fleep.TypeClasses.File(fc);
                FileInfoList fl;

                fl = f.FileUpload(txtFilename.Text,txtConvID.Text);

                txtLog.Text = "";
                log(fl.ToJSONPrint());
                et(s);

            }
            catch (Exception ex)
            {
                log(ex.ToString());
            }

        }

    }
}
