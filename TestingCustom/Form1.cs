using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailLibrary;

namespace TestingCustom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected string GmailConfiguration1 = "mailSettings/smtp_1";
        protected string GmailConfiguration2 = "mailSettings/smtp_2";
        protected string Configuration1 = "mailSettings/smtp_3";
        protected string ComcastConfiguration = "system.net/mailSettings/smtp";
        protected string LogfileName = "Demo.txt";
        protected string SendMessageToAddress = "paynekaren@comcast.net";
        private void button1_Click(object sender, EventArgs e)
        {
            var ops = new Operations(true, LogfileName);
            ops.ExampleSendCustomClient(GmailConfiguration2, SendMessageToAddress, 4);
        }
    }
}
