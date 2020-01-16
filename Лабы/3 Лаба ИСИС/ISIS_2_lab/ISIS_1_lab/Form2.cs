using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace ISIS_1_lab
{
    public partial class Form2 : Form
    {
        char[] array = new char[33] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        int[] quantity = new int[34];
        SqlConnection sqlConnection;
        public void Sqlcon()
        {
            string ConnectionString = @"Data Source = PC2-201-3\SQLEXPRESS; Initial Catalog = ISIDB; Integrated Security = True";
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
        }
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ColumnCount = 3;
            dataGridView2.Columns[0].HeaderText = "Длина слова";
            dataGridView2.Columns[1].HeaderText = "Кол-во слов";
            dataGridView2.Columns[2].HeaderText = "Частота встречи, %";
            dataGridView2.Columns[2].DefaultCellStyle.Format = "n2";
            Authorization aut = new Authorization();
            Sqlcon();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT name FROM [dbo].[Texts]", sqlConnection);
            string str = "";
            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    str += (Convert.ToString(sqlReader["name"]) + ";");
                }
                string[] names = str.Split(new char[] { ';' });
                foreach (string name in names)
                    comboBox1.Items.Add(name);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            Sqlcon();
            //    SqlDataReader sqlReader1 = null;
            //    SqlCommand command1 = new SqlCommand("SELECT name FROM [dbo].[IniFiles]", sqlConnection);
            //    string str1 = "";
            //    try
            //    {
            //        sqlReader1 = command1.ExecuteReader();
            //        while (sqlReader1.Read())
            //        {
            //            str1 += (Convert.ToString(sqlReader1["name"]) + ";");
            //        }
            //        string[] names = str1.Split(new char[] { ';' });
            //        foreach (string name in names)
            //            comboBox2.Items.Add(name);
            //    }
            //    finally
            //    {
            //        if (sqlReader1 != null)
            //            sqlReader1.Close();
            //    }
            //}
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
                int ch = arg(Convert.ToInt32(textBox2.Text[i]));
                if (ch > -1)
                {
                    quantity[ch]++;
                    quantity[33]++;
                }
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            chart2.Series["Количество букв"].Points.Clear();
            dataGridView2.Rows.Clear();
            int leit = textBox2.TextLength;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            await Task.Run(() => doing(leit));
            textBox2.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            int j = 0;
            for (int i = 0; i < 33; i++)
            {
                if (quantity[i] > 0)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[j].Cells[0].Value = array[i];
                    dataGridView2.Rows[j].Cells[1].Value = quantity[i];
                    chart2.Series["Количество букв"].Points.AddXY(array[i].ToString(), quantity[i]);
                    chart2.ChartAreas[0].AxisX.Interval = 1;
                    j++;
                }
            }
            dataGridView2.Rows[j].Cells[0].Value = "Всего ру-букв";
            dataGridView2.Rows[j].Cells[1].Value = quantity[33];
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            textBox2.Text = fileText;
        }




        private void Button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog o1 = new OpenFileDialog();
            o1.Filter = "INI File |*.ini";
            if (o1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string a = ini_read(o1.FileName);
            }
        }
        public string ini_read(string FileName)
        {
            IniFiles ini = new IniFiles(FileName);
            string ini1 = ini.ReadINI("App", "Value_button1");
            string ini2 = ini.ReadINI("App", "Value_text");
            string ini3 = ini.ReadINI("App", "Value_button2");
            button1.Text = ini1;
            textBox2.Text += ini2;
            button2.Text = ini3;
            return ini3;
        }


        private void Button8_Click(object sender, EventArgs e)
        {
            Sqlcon();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT text FROM [dbo].[Texts] WHERE name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", comboBox1.Text);
            string str = "";
            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    str = (Convert.ToString(sqlReader["text"]));

                }
                if (str != "")
                {
                    textBox2.Text = str;
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

        }

        private void Button9_Click(object sender, EventArgs e)
        {

            Sqlcon();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT COUNT(name) FROM [dbo].[IniFiles]", sqlConnection);
            string strid = "";
            int intid;
            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    strid = (Convert.ToString(sqlReader[""]));

                }
                intid = Convert.ToInt32(strid);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            Sqlcon();
            SqlCommand command1 = new SqlCommand("INSERT INTO [dbo].[IniFiles](id, name, value_text, value_button1, value_button2) VALUES(@id, @name, @value_text, @value_button1, @value_button2)", sqlConnection);
            command1.Parameters.AddWithValue("id", intid);
            command1.Parameters.AddWithValue("name", textBox1.Text);
            command1.Parameters.AddWithValue("value_text", textBox2.Text);
            command1.Parameters.AddWithValue("value_button1", button1.Text);
            command1.Parameters.AddWithValue("value_button2", button2.Text);
            command1.ExecuteNonQuery();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Sqlcon();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[IniFiles] WHERE name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", comboBox2.Text);
            string str = "";
            try
            {
                sqlReader = command.ExecuteReader();
                while (sqlReader.Read())
                {
                    textBox2.Text = (Convert.ToString(sqlReader["value_text"]));
                    button1.Text = (Convert.ToString(sqlReader["value_button1"]));
                    button2.Text = (Convert.ToString(sqlReader["value_button2"]));
                }
                if (str != "")
                {
                    textBox2.Text = str;
                }
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            int a = ochist();
            if (a != -1)
            {
                MessageBox.Show("Произошла ошибка очистки");
            }
            //button1.Enabled = true;
        }
        public int ochist()
        {
            try
            {
                textBox2.Clear();
                dataGridView2.Rows.Clear();
                chart2.Series.Clear();
                return -1;
            }
            catch
            {
                return 0;
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
                DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
                dt.TableName = "Статистика"; // название таблицы
                dt.Columns.Add("Длина слова"); // название колонок
                dt.Columns.Add("Количество слов");
                dt.Columns.Add("Частота встречи");
                ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

                foreach (DataGridViewRow r in dataGridView2.Rows) // пока в dataGridView2 есть строки
                {
                    DataRow row = ds.Tables["Статистика"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                    row["Длина слова"] = r.Cells[0].Value;  //в столбец этой строки заносим данные из первого столбца dataGridView2
                    row["Количество слов"] = r.Cells[1].Value; // то же самое со вторыми столбцами
                    row["Частота встречи"] = r.Cells[2].Value; //то же самое с третьими столбцами
                    ds.Tables["Статистика"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
                }
                ds.WriteXml("E:\\Data.xml");
                MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveINI = new SaveFileDialog();
            saveINI.Filter = "INI File |*.ini";
            if (saveINI.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IniFiles ini = new IniFiles(saveINI.FileName);
                ini.Write("App", "Value_button1", button1.Text);
                ini.Write("App", "Value_text", textBox2.Text);
                ini.Write("App", "Value_button2", button2.Text);
            }
        }
        public int zagr()
        {
            int a1 = 0;
            return a1;
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                Sqlcon();
                SqlDataReader sqlReader = null;
                SqlCommand command = new SqlCommand("SELECT COUNT(name) FROM [dbo].[Texts]", sqlConnection);
                string strid = "";
                int intid;
                try
                {
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        strid = (Convert.ToString(sqlReader[""]));

                    }
                    intid = Convert.ToInt32(strid);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
                Sqlcon();
                SqlCommand command1 = new SqlCommand("INSERT INTO [dbo].[Texts](id, name, text) VALUES(@id, @name, @text)", sqlConnection);
                command1.Parameters.AddWithValue("id", intid);
                command1.Parameters.AddWithValue("name", comboBox1.Text);
                command1.Parameters.AddWithValue("text", textBox2.Text);
                command1.ExecuteNonQuery();
            }
        }

       
    }
}
    
