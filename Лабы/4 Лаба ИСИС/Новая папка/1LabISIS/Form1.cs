using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1labISIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int i = 1;
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            IsMdiContainer = true;
            Child newForm = new Child();
            newForm.MdiParent = this;
            newForm.Text = "Child " + i;
            i++;
            newForm.Show();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] form = MdiChildren;
            foreach (Form f in form)
                f.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void VerticalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void HorizontalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void MozaikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] form = MdiChildren;
            foreach (Form f in form)
                f.WindowState = FormWindowState.Minimized;
        }

        private void OrderingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            Form[] form = MdiChildren;
            foreach (Form f in form)
            {
                f.StartPosition = FormStartPosition.Manual;
                f.Location = new Point(i);
                i += 100;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
