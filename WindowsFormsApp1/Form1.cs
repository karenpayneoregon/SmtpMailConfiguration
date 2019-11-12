using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailDefinition md = new MailDefinition
            {
                From = "test@domain.com",
                IsBodyHtml = true,
                Subject = "Test of MailDefinition"
            };

            ListDictionary replacements = new ListDictionary {{"{name}", "Martin"}, {"{country}", "Denmark"}};

            string body = "<div>Hello {name} You're from {country}.</div>";
            MailMessage msg = md.CreateMailMessage("you@anywhere.com", replacements, body, new System.Web.UI.Control());
            
            Console.WriteLine(msg.Body);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var userName = "Bill";
            var writer = new StringWriter();
            var html = new HtmlTextWriter(writer);

            html.RenderBeginTag(HtmlTextWriterTag.H1);
            
            html.WriteEncodedText("Heading Here");
            html.RenderEndTag();
            html.WriteEncodedText($"Dear {userName}");
            html.WriteBreak();
            html.RenderBeginTag(HtmlTextWriterTag.P);
            html.WriteEncodedText("First part of the email body goes here");
            html.RenderEndTag();
            html.Flush();

            string htmlString = writer.ToString();
            Console.WriteLine(htmlString);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        PayneMail demo = new PayneMail().From("FromAddy", "FromDisplay").To("ToEmailAddy", "ToDisplay");
        Console.WriteLine(demo.GetResult().ToDisplayName);
        Console.WriteLine(demo.GetResult().FromDisplayName);

        }
    }
}
