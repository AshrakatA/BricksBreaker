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
    public partial class GameIntro : Form
    {
        System.Windows.Media.MediaPlayer mp = new System.Windows.Media.MediaPlayer();

        public GameIntro()
        {
            mp.Open(new Uri(@"dependencies/Epic Chinese Music - Kung Fu.wav", UriKind.Relative));
            mp.Play();
            mp.MediaEnded += mp_MediaEnded;
            InitializeComponent();
        }

        void mp_MediaEnded(object sender, EventArgs e)
        {
            mp.Position = TimeSpan.Zero;
            mp.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientHost ch = new ClientHost();
          //  this.Visible = false;
            Hide();
            ch.Show();
            
        }

        private void GameIntro_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  Application.Exit();
        }

        private void GameIntro_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
