using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SphereScp
{
    public partial class TabTest : Form
    {
        public TabTest()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        int xx = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            MSATabPage tpag = new MSATabPage(new TextBox() { Multiline = true }, "test");
            ContextMenuStrip stp = new ContextMenuStrip();
            stp.Name = "cntMSATabPage" + xx;
            stp.Items.Add(new ToolStripMenuItem() { Name = "kapatToolStripMenuItem" + xx, Text = "Kapat" });
            msaTabControl2.AddPage(tpag, stp);
            xx++;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
