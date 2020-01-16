using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace mdi
{
    public partial class Form2 : Form
    {

        char[] array = new char[33] {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ','ы','ь','э', 'ю', 'я' };
        int[] quantity = new int[34];
        public Form2()
        {
            InitializeComponent();
        }
        
    
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private int arg(int ch)//получение индексов букв
        {
            
            if (ch == 1105 || ch == 1025) return 6; //буква ё БОЛЬШИЕ и маленькие
            if (ch > 1039 && ch < 1046) return ch - 1040;// от а до е маленькие
            if (ch > 1071 && ch < 1078) return ch - 1072;// от е до я маленькие
            if (ch > 1045 && ch < 1072) return ch - 1039;// от а до е БОЛЬШИЕ
            if (ch > 1077 && ch < 1104) return ch - 1071;// от е до я БОЛЬШИЕ
            return -1; // нерусские символы
        }
        public void doing(int leit)
        {
            quantity = new int[34];
            for (int i = 0; i < leit; i++)
            {
                int ch = arg(Convert.ToInt32(textBox1.Text[i])); 
                if (ch > -1) 
                { 
                    quantity[ch]++;    
                    quantity[33]++; 
                }
            }
        }


        public async void button1_Click(object sender, EventArgs e)
        {
   
            chart1.Series["Количество букв"].Points.Clear();
            dataGridView1.Rows.Clear();
            int leit = textBox1.TextLength;
            textBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            await Task.Run(() => doing(leit));
            textBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            int j = 0;
            for (int i = 0; i < 33; i++)
            {
                if (quantity[i] > 0)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[j].Cells[0].Value = array[i];
                    dataGridView1.Rows[j].Cells[1].Value = quantity[i];
                    chart1.Series["Количество букв"].Points.AddXY(array[i].ToString(), quantity[i]);
                    chart1.ChartAreas[0].AxisX.Interval = 1;
                    j++;
                }
            }
            dataGridView1.Rows[j].Cells[0].Value = "Всего ру-букв";
            dataGridView1.Rows[j].Cells[1].Value = quantity[33];
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
          
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            textBox1.Text = fileText;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
