using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace musicplayer
{
    public partial class Functiontest : Form
    {
        string path;
        public OpenFileDialog file = new OpenFileDialog();
        string filename;
        bool? issongplaying;
        bool? isplaylistopen;
        public static Functiontest instance;
        public Functiontest()
        {
            InitializeComponent();
            instance = this;
        }
        public void taglib()
        {
            var fw = TagLib.File.Create(@path);
            string title = fw.Tag.Title;
            string[] art = new string[5];
            art = fw.Tag.Performers;
            uint year = fw.Tag.Year;
            string album = fw.Tag.Album;
            label5.Text = title;
            label6.Text = string.Join("", art);
            label7.Text = album;
            label8.Text = year==0 ? "" : Convert.ToString(year);
            var mStream = new MemoryStream();
            var firstPicture = fw.Tag.Pictures.FirstOrDefault();
            if (firstPicture != null)
            {
                byte[] pData = firstPicture.Data.Data;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                var bm = new Bitmap(mStream, false);
                mStream.Dispose();
                pictureBox1.Image = bm;
            }
            if (firstPicture == null)
            {
                pictureBox1.Image = pictureBox1.InitialImage;
            }
        }
        public void progress()
        {
            p_bar.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
            p_bar.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
        }
        public void time()
        {
            label9.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            label10.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString.ToString();
        }
        private void Functiontest_Load(object sender, EventArgs e)
        {
            //do nothing (Existing for callback value only)
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while(true)
            {
                if (issongplaying == true)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                    button1.Text = "4";
                    issongplaying = false;
                    break;

                }
                if (issongplaying == false)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    button1.Text = ";";
                    issongplaying = true;
                    break;
                }
                if(issongplaying==null)
                {
                    break;
                }
            }
        }
        public void button2_Click(object sender, EventArgs e)
        {
            if(DialogResult.OK==file.ShowDialog())
            {
                path = file.FileName;
                axWindowsMediaPlayer1.URL = path;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                button1.Text = ";";
            }
            taglib();
            issongplaying = true;
            isplaylistopen = false;
            filename= System.IO.Path.GetFileName(file.FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            button1.Text = "4";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            pictureBox1.Image = pictureBox1.InitialImage;
            issongplaying = null;
            p_bar.Value = 0;
            p_bar.Maximum = 0;
            label10.Text = "";
            label9.Text = "";
            isplaylistopen = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (issongplaying == true)
            {
                progress();
                time();
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if(axWindowsMediaPlayer1.playState==WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                p_bar.Value = 0;
                p_bar.Maximum = 0;
                label10.Text = "";
                label9.Text = "";
                button1.Text = "4";
                issongplaying = false;
            }
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                timer1.Start();
                issongplaying = true;
            }
        }

        private void p_bar_MouseDown(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.currentMedia.duration * e.X / p_bar.Width;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.next();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            Form2.instance.lb2.Text = filename;
            while (true)
            {
                if(isplaylistopen==false)
                {
                    a.Show();
                    isplaylistopen = true;
                    break;
                }
                else if(isplaylistopen==true)
                {
                    a.Close();
                    isplaylistopen = false;
                    break;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
