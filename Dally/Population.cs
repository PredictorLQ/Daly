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
            List<DataSetDaly> DataSetDaly = DataDaly.DataSetDaly.Where(u => u.Year == DataDaly.ActivDataYear_Id && u.DataRegion_Id == DataDaly.ActivDataRegion_Id).ToList();
            foreach (var item in DataDaly.DataPopulation)
            {
                if(item.Excel == true)
                {
                    DataSetDaly data = DataSetDaly.First(u => u.DataPopulation_Id == item.Id);
                    dataGridView1.Rows.Add(item.Name, data.MaleLife, data.MaleDied, data.MaleBirth, item.PeriodDied);
                    dataGridView2.Rows.Add(item.Name, data.FemaleLife, data.FemaleDied, data.FemaleBirth, item.PeriodDied);
                }
            }
        }

        private void Population_Load(object sender, EventArgs e)
        {

        }
    }
}
