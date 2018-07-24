using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class ClientHost : Form
    {
        public static bool player1;
        public ClientHost()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox2.Enabled = false;
                groupBox1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                
                //open game as a host
                int myPortNum = int.Parse(textBox1.Text);
                GameForm gf = new GameForm(myPortNum);
                gf.player1=true;
                Visible = false;
                gf.Show();
            }
            //open game as a client
            else
            {
                
                int hostPortNum = int.Parse(textBox2.Text);
                String hostIP = textBox3.Text;
                GameForm gf = new GameForm(hostIP, hostPortNum);
                gf.player1 = false;
                Visible=false;
                gf.Show();  
            }
        }

        private void ClientHost_FormClosing(object sender, FormClosingEventArgs e)
        {
       //     Environment.Exit(0);
        }

        private void ClientHost_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
