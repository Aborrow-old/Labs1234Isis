using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string firsttext = "";
        private void Button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Текстовые файлы (*.rtf; *.txt; *.dat) | *.rtf; *.txt; *.dat";
            openFileDialog1.Title = "Загрузить из...";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                firsttext = openFileDialog1.FileName;
                try
                {
                    richTextBox1.LoadFile(firsttext);
                }
                catch
                {
                    richTextBox1.Text = File.ReadAllText(firsttext);
                }
            }
        }
    }
}
