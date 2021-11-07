using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace musicplayer
{
    public partial class Form2 : Form
    {
        public static Form2 instance;
        public Label lb2;
        public Form2()
        {
            InitializeComponent();
            instance = this;
            lb2 = label1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
