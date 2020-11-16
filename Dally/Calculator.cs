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
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            List<DataSetDaly> DataSetDaly = DataDaly.DataSetDaly.Where(u => u.Year == DataDaly.ActivDataYear_Id && u.DataRegion_Id == DataDaly.ActivDataRegion_Id).ToList();
            foreach (var item in DataDaly.DataPopulation)
            {
                if (item.Start_Daly_Bool == true)
                {
                    DataSetDaly data = DataSetDaly.First(u => u.DataPopulation_Id == item.Id);
                    DataSetDalyDiases diases = data.DataSetDalyDiases.First(u => u.DataDiases_Id == DataDaly.ActivDataDiases_Id);
                    dataGridView1.Rows.Add(item.Name, diases.DataSurvivalMale.e0_2, diases.DataSurvivalMale.YLL);
                    dataGridView2.Rows.Add(item.Name, diases.DataSurvivalFemale.e0_2, diases.DataSurvivalFemale.YLL);
                }
            }
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }
    }
}
