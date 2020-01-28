using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Многоугольники
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            trackBar1.Value = Shape.GetR;
        }

        public delegate void RadiusContainer(object sender, REventArgs e);

        public event RadiusContainer RChange;

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
                RChange(this, new REventArgs(trackBar1.Value));
        }
    }
}
