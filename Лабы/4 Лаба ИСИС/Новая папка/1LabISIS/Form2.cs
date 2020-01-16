using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace _1labISIS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:64195/");
                HttpResponseMessage response = client.GetAsync("api/products").Result;
                var setting = response.Content.ReadAsAsync<IEnumerable<Settings>>().Result;
                serviceGrid.DataSource = setting;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                AddProduct(textBox2.Text, textBox3.Text);
                MessageBox.Show("Настройка добавлена");
                textBox2.Text = "";
                textBox3.Text = "";
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddProduct(string name, string val)
        {
            Settings s = new Settings();
            s.Name = name;
            s.Value = val;
            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(s);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("http://localhost:64195/api/products", content);
            };
        }

        private async void DeleteProduct(int delid)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync(String.Format("{0}/{1}", "http://localhost:64195/api/products", delid));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteProduct(Convert.ToInt32(textBox1.Text));
                MessageBox.Show("Настройка удалена");
                textBox1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile("settings.ini");
            int formWidth = Convert.ToInt32(ini.Read("FormWidth"));
            int formHeight = Convert.ToInt32(ini.Read("FormHeight"));
            int width = Convert.ToInt32(ini.Read("ButtonWidth"));
            int height = Convert.ToInt32(ini.Read("ButtonHeight"));
            int width1 = Convert.ToInt32(ini.Read("but1w"));
            int height1 = Convert.ToInt32(ini.Read("but1h"));
            int width2 = Convert.ToInt32(ini.Read("but2w"));
            int height2 = Convert.ToInt32(ini.Read("but2h"));
            int width3 = Convert.ToInt32(ini.Read("but3w"));
            int height3 = Convert.ToInt32(ini.Read("but3h"));
            string firstpath = ini.Read("Pathfirst");
            string secondpath = ini.Read("Pathsecond");
            AddProduct("FormWidth", Convert.ToString(formWidth));
            AddProduct("FormHeight", Convert.ToString(formHeight));
            AddProduct("ButtonWidth", Convert.ToString(width));
            AddProduct("ButtonHeight", Convert.ToString(height));
            AddProduct("but1w", Convert.ToString(width1));
            AddProduct("but1h", Convert.ToString(height1));
            AddProduct("but2w", Convert.ToString(width2));
            AddProduct("but2h", Convert.ToString(height2));
            AddProduct("but3w", Convert.ToString(width3));
            AddProduct("but3h", Convert.ToString(height3));
            AddProduct("Pathfirst", Convert.ToString(firstpath));
            AddProduct("Pathsecond", Convert.ToString(secondpath));
        }
    }
}
