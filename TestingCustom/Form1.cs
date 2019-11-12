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
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ActiveControl = button2;
        }

        protected string GmailConfiguration1 = "mailSettings/smtp_1";
        protected string GmailConfiguration2 = "mailSettings/smtp_2";
        protected string Configuration1 = "mailSettings/smtp_3";
        protected string ConfigurationOed = "mailSettings/smtp_4";
        protected string ComcastConfiguration = "system.net/mailSettings/smtp";
        protected string LogfileName = "Demo.txt";
        protected string SendMessageToAddress = "karen.1.payne@oregon.gov";
        private void button1_Click(object sender, EventArgs e)
        {
            var ops = new Operations(true, LogfileName);
            ops.ExampleSendCustomClient(ConfigurationOed, SendMessageToAddress, 5);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ops = new Operations(true, LogfileName);
            ops.OED_1(ConfigurationOed, SendMessageToAddress, 5);
        }
    }
}
