using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISIS_1_lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Soska();
        }

        SqlConnection sqlConnection;
        public void Sqlcon()
        {
            string ConnectionString = @"Data Source = PC2-201-3\SQLEXPRESS; Initial Catalog = ISIDB; Integrated Security = True";
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
        }
        private void VerticalTieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            Form2 newForm = new Form2();
            newForm.MdiParent = this;
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

        private void HorizontalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void Soska()
        {
            Sqlcon();
            if (Convert.ToInt32(new SqlCommand("SELECT COUNT(*) FROM [dbo].[IniFiles]", sqlConnection).ExecuteScalar()) != 0)
            {
                SqlCommand command = new SqlCommand("SELECT @LocX=LocX, @LocY=LocY, @SizeH=SizeH, @SizeW=SizeW FROM [dbo].[IniFiles]", sqlConnection);
                command.Parameters.Add(new SqlParameter("LocX", SqlDbType.Int)).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("LocY", SqlDbType.Int)).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("SizeH", SqlDbType.Int)).Direction = ParameterDirection.Output;
                command.Parameters.Add(new SqlParameter("SizeW", SqlDbType.Int)).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                int x = Convert.ToInt32(command.Parameters["LocX"].Value);
                int y = Convert.ToInt32(command.Parameters["LocY"].Value);
                int h = Convert.ToInt32(command.Parameters["SizeH"].Value);
                int w = Convert.ToInt32(command.Parameters["SizeW"].Value);
                this.Location = new Point(x, y);
                this.Size = new Size(w, h);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Sqlcon();
            SqlCommand command1 = new SqlCommand("INSERT INTO [dbo].[IniFiles](LocX, LocY, SizeH, SizeW) VALUES(@LocX, @LocY, @SizeH, @SizeW)", sqlConnection);
            command1.Parameters.AddWithValue("LocX", Convert.ToString(this.Location.X));
            command1.Parameters.AddWithValue("LocY", Convert.ToString(this.Location.Y));
            command1.Parameters.AddWithValue("SizeH", Convert.ToString(this.Size.Height));
            command1.Parameters.AddWithValue("SizeW", Convert.ToString(this.Size.Width));
            command1.ExecuteNonQuery();
        }
    }
}
