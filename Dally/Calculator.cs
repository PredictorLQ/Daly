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
            List<DataSetDaly> DataSetDaly = DataDaly.DataSetDaly.Where(u => DataDaly.ActivDataYear_Id.Any(t => t == u.Year) == true
            && DataDaly.ActivDataRegion_Id.Any(t => t == u.DataRegion_Id) == true).ToList();
            (double, double) vrp_all = (0, 0);
            foreach (var item in DataDaly.DataPopulation)
            {
                if (item.Start_Daly_Bool == true)
                {
                    List<DataSetDalyDiases> diases = new List<DataSetDalyDiases>();
                    List<DataSetDaly> data = DataSetDaly.Where(u => u.DataPopulation_Id == item.Id).ToList();

                    for (int i = 0; i < data.Count; i++)
                    {
                        diases.AddRange(data[i].DataSetDalyDiases.Where(u => DataDaly.ActivDataDiases_Id.Any(t => t == u.DataDiases_Id)).ToList());
                    }
                    (double, double) vrp = (diases.Average(t => t.DataSurvivalMale.VRP), diases.Average(t => t.DataSurvivalFemale.VRP));
                    if (item.Id == 19)
                    {
                        vrp.Item1 /= 2.0;
                        vrp.Item2 /= 2.0;
                    }
                    vrp_all.Item1 += vrp.Item1;
                    vrp_all.Item2 += vrp.Item2;
                    dataGridView1.Rows.Add(item.Name, diases.Average(t=>t.DataSurvivalMale.e0_2), diases.Average(t => t.DataSurvivalMale.YLL), vrp.Item1);
                    dataGridView2.Rows.Add(item.Name, diases.Average(t => t.DataSurvivalFemale.e0_2), diases.Average(t => t.DataSurvivalFemale.YLL), vrp.Item2);
                }
            }
            dataGridView1.Rows.Add("Итого", "", "", vrp_all.Item1);
            dataGridView2.Rows.Add("Итого", "", "", vrp_all.Item2);
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }
    }
}
