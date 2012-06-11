using System;
using System.Linq;
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

        private void BtnAddClick(object sender, EventArgs e)
        {
            var point = new D3.Point(Convert.ToInt32(txtX.Text), Convert.ToInt32(txtY.Text));
            var waypoint = new Common.Waypoint(point, 
                (D3.SNOLevelArea)Convert.ToInt32(txtLevelArea.Text), 
                (D3.SNOQuestId)Convert.ToInt32(txtQuestID.Text), 
                Convert.ToInt16(txtQuestStep.Text));
            lstCoordinates.Items.Add(waypoint);
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            var waypoints = lstCoordinates.Items.Cast<Common.Waypoint>().ToArray();
            var serializer = new XmlSerializer(typeof(Common.Waypoint[]));
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == "") return;
            TextWriter textWriter = new StreamWriter(saveFileDialog1.FileName);
            serializer.Serialize(textWriter, waypoints);
            textWriter.Close();
        }
    }
}
