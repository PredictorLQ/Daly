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

namespace Daly
{
    public partial class Population : Form
    {
        public Population()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            List<DataSetDaly> DataSetDaly = DataDaly.DataSetDaly.Where(u => DataDaly.ActivDataYear_Id.Any(t => t == u.Year) == true
            && DataDaly.ActivDataRegion_Id.Any(t => t == u.DataRegion_Id) == true).ToList();
            foreach (var item in DataDaly.DataPopulation)
            {
                if (item.Excel == true)
                {
                    List<DataSetDaly> data = DataSetDaly.Where(u => u.DataPopulation_Id == item.Id).ToList();
                    dataGridView1.Rows.Add(item.Name, data.Sum(u => u.MaleLife), data.Sum(u => u.MaleDied), data.Sum(u => u.MaleBirth), item.PeriodDied);
                    dataGridView2.Rows.Add(item.Name, data.Sum(u => u.FemaleLife), data.Sum(u => u.FemaleDied), data.Sum(u => u.FemaleBirth), item.PeriodDied);
                }
            }
        }

        private void Population_Load(object sender, EventArgs e)
        {

        }
    }
}
