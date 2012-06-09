using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snowshoes.UI
{
    public partial class DebugUI : Form
    {
        public DebugUI()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            D3.Point point = new D3.Point(Convert.ToInt32(txtX.Text), Convert.ToInt32(txtY.Text));
            lstCoordinates.Items.Add(point);
        }
    }
}
