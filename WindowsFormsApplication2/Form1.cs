using System;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailConfiguration mc = new MailConfiguration();
            Console.WriteLine(mc.DefaultCredentials);
            Console.WriteLine(mc.HostServer);
            Console.WriteLine(mc.Port);
        }
    }
}
