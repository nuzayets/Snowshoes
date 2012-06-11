using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

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
            Common.Waypoint waypoint = new Common.Waypoint(point, 
                (D3.SNOLevelArea)Convert.ToInt32(txtLevelArea.Text), 
                (D3.SNOQuestId)Convert.ToInt32(txtQuestID.Text), 
                Convert.ToInt16(txtQuestStep.Text));
            lstCoordinates.Items.Add(waypoint);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Common.Waypoint[] waypoints = lstCoordinates.Items.Cast<Common.Waypoint>().ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(Common.Waypoint[]));
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                TextWriter textWriter = new StreamWriter(saveFileDialog1.FileName);
                serializer.Serialize(textWriter, waypoints);
                textWriter.Close();
            }
        }
    }
}
