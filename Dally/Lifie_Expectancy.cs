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
    public partial class Lifie_Expectancy : Form
    {
        public Lifie_Expectancy()
        {
            InitializeComponent();
            if (dataGridView1.Rows.Count <= 0)
            {
            }
        }

        private void Lifie_Expectancy_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
               
            }
            MessageBox.Show("Данные были приняты");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
