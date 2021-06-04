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
            dataGridView3.Rows.Clear();
            label8.Text = $"( {DataDaly.SelectPaketName[DataDaly.SelectPaket - 1]} )";
            label8.Visible = true;
            List<DataSetDaly> DataSetDaly = DataDaly.DataSetDaly.Where(u => DataDaly.ActivDataYear_Id.Any(t => t == u.Year) == true
            && DataDaly.ActivDataRegion_Id.Any(t => t == u.DataRegion_Id) == true).ToList();
            (double, double, double) vrp_all = (0, 0, 0);
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
                    (double, double, double) vrp = (diases.Average(t => t.DataSurvivalMale.VRP), diases.Average(t => t.DataSurvivalFemale.VRP), diases.Average(t => t.DataSurvivalSumm.VRP));
                    if (item.Id == 19)
                    {
                        vrp.Item1 /= 2.0;
                        vrp.Item2 /= 2.0;
                        vrp.Item3 /= 2.0;
                    }
                    vrp_all.Item1 += vrp.Item1;
                    vrp_all.Item2 += vrp.Item2;
                    vrp_all.Item3 += vrp.Item3;
                    dataGridView1.Rows.Add(item.Name, diases.Average(t => t.DataSurvivalMale.e0_2), diases.Average(t => t.DataSurvivalMale.YLL), diases.Average(t => t.DataSurvivalMale.YLL100000), vrp.Item1);
                    dataGridView2.Rows.Add(item.Name, diases.Average(t => t.DataSurvivalFemale.e0_2), diases.Average(t => t.DataSurvivalFemale.YLL), diases.Average(t => t.DataSurvivalFemale.YLL100000), vrp.Item2);
                    dataGridView3.Rows.Add(item.Name, diases.Average(t => t.DataSurvivalSumm.e0_2), diases.Average(t => t.DataSurvivalSumm.YLL), diases.Average(t => t.DataSurvivalSumm.YLL100000), vrp.Item3);
                }
            }
            dataGridView1.Rows.Add("Итого", "", "", vrp_all.Item1);
            dataGridView2.Rows.Add("Итого", "", "", vrp_all.Item2);
            dataGridView3.Rows.Add("Итого", "", "", vrp_all.Item3);
            List<int> max_count = new List<int>{
            DataDaly.ActivDataYear_Id.Count,
            DataDaly.ActivDataRegion_Id.Count,
            DataDaly.ActivDataDiases_Id.Count
            };
            int max = max_count.Max();
            for (int i = 0; i < max; i++)
            {
                (string, string, string) elem = ("", "", "");
                try
                {
                    DataDiases DataDiases = DataDaly.DataDiases.First(u => u.Id == DataDaly.ActivDataDiases_Id[i]);
                    elem.Item1 = $"{DataDiases.MCB10} {DataDiases.Name}";
                }
                catch { }
                try
                {
                    elem.Item2 = DataDaly.DataRegion.First(u => u.Id == DataDaly.ActivDataRegion_Id[i]).Name;
                }
                catch { }
                try
                {
                    elem.Item3 = DataDaly.ActivDataYear_Id[i].ToString();
                }
                catch { }
                dataGridView7.Rows.Add(elem.Item1, elem.Item2, elem.Item3);
            }
        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }
    }
}
