using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daly
{
    public partial class Constant : Form
    {
        public Constant()
        {
            InitializeComponent();
            textBox10.Text = DataDalyConstant.Discount_rate.ToString();
            textBox1.Text = DataDalyConstant.Beta.ToString();
            textBox2.Text = DataDalyConstant.Constant_C.ToString();
            textBox3.Text = DataDalyConstant.Constant_K.ToString();
            textBox4.Text = DataDalyConstant.Constant_N.ToString();
        }

        private void Constant_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { DataDalyConstant.Discount_rate = double.Parse(textBox10.Text.Replace(".", ",")); }
            catch { }
            try { DataDalyConstant.Beta = double.Parse(textBox1.Text.Replace(".", ",")); }
            catch { }
            try { DataDalyConstant.Constant_C = double.Parse(textBox2.Text.Replace(".", ",")); }
            catch { }
            try { DataDalyConstant.Constant_K = double.Parse(textBox3.Text.Replace(".", ",")); }
            catch { }
            try { DataDalyConstant.Constant_N = double.Parse(textBox4.Text.Replace(".", ",")); }
            catch { }
            MessageBox.Show("Данные были приняты");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
