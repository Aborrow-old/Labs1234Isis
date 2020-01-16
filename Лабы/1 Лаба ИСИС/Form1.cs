using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace mdi
{
    public partial class Form1 : Form
    {
       
        public class INIManager
        {
            
            public INIManager(string aPath)
            {
                path = aPath;
            }

           
            public INIManager() : this("") { }

           
            public string GetPrivateString(string aSection, string aKey)
            {
                
                StringBuilder buffer = new StringBuilder(SIZE);

               
                GetPrivateString(aSection, aKey, null, buffer, SIZE, path);

                
                return buffer.ToString();
            }

           
            public void WritePrivateString(string aSection, string aKey, string aValue)
            {
               
                WritePrivateString(aSection, aKey, aValue, path);
            }


            public string Path { get { return path; } set { path = value; } }

            //Поля класса
            private const int SIZE = 1024;
            private string path = null;


            [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
            private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

            [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
            private static extern int WritePrivateString(string section, string key, string str, string path);
        }

        public Form1()
        {
            InitializeComponent();
           
        }
        INIManager manager;
        XDocument doc;
        private void save_razmeri()
        {
            manager.WritePrivateString("size", "height", Convert.ToString(this.Height));
            manager.WritePrivateString("size", "width", Convert.ToString(this.Width));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int k = 0;
            bool isINI =File.Exists(Application.StartupPath + "\\my.ini");
            manager = new INIManager(Application.StartupPath + "\\my.ini");
            if (isINI)
            { 
                k = Convert.ToInt32(manager.GetPrivateString("formi", "kol")); 
                this.Height = Convert.ToInt32(manager.GetPrivateString("size", "height"));
                this.Width = Convert.ToInt32(manager.GetPrivateString("size", "width"));
            }
            else
            { 
                save_razmeri();
            }
           
            doc = new XDocument();
            XmlTextReader reader = new XmlTextReader(Application.StartupPath + "\\my.xml");
            this.IsMdiContainer = true; 
            int d = 0; 
           
            while (d!=k && reader.Read()) 
            {
                if (reader.NodeType == XmlNodeType.Text)
                    {
                        Form2 cf = new Form2();
                        cf.MdiParent=this;
                        cf.textBox1.Text = reader.Value;
                        Point loc = cf.Location; 
                        loc.X = Convert.ToInt32(manager.GetPrivateString("dochi" + d.ToString(), "LocX"));
                        loc.Y = Convert.ToInt32(manager.GetPrivateString("dochi" + d.ToString(), "LocY"));
                        cf.Location = loc;

                        Size siz=cf.Size;
                        siz.Height = Convert.ToInt32(manager.GetPrivateString("dochi" + d.ToString(), "SizeH"));
                        siz.Width = Convert.ToInt32(manager.GetPrivateString("dochi" + d.ToString(), "SizeW"));
                        cf.Size = siz;
                        cf.Show();
                        d++;  
                    }
            }
            reader.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            Form2 newForm = new Form2();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] form = MdiChildren;
            foreach (Form f in form) f.Close();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void verticalTiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void horizontalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            save_razmeri();
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            doc = new XDocument();
            Form[] dochi = this.MdiChildren;
            int k = dochi.Count();
            manager.WritePrivateString("formi", "kol", Convert.ToString(k));
            XElement texts = new XElement("formi");
            for (int i = 0; i < k; i++)
            {
                XElement text = new XElement("Text" + i.ToString(), ((TextBox)dochi[i].Controls["textBox1"]).Text);
                manager.WritePrivateString("dochi" + i.ToString(), "LocX", Convert.ToString(dochi[i].Location.X));
                manager.WritePrivateString("dochi" + i.ToString(), "LocY", Convert.ToString(dochi[i].Location.Y));
                manager.WritePrivateString("dochi" + i.ToString(), "SizeH", Convert.ToString(dochi[i].Size.Height));
                manager.WritePrivateString("dochi" + i.ToString(), "SizeW", Convert.ToString(dochi[i].Size.Width));
                texts.Add(text); 
            }
            doc.Add(texts);
            doc.Save(Application.StartupPath + "\\my.xml");
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
