using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Packet
{
    public partial class PortConfig : Form
    {
        public PortConfig()
        {
            InitializeComponent();
        }
        //public PortConfig(SelectPort selPort)
        //{
        //    InitializeComponent();
        //    lblPortName.Text = selPort._SP1.PortName;

        //}

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
